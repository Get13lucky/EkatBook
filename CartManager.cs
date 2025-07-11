using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EkatBooks
{
    public class CartManager
    {
        private static string connectionString = "Host=localhost;Port=5432;Database=Books;Username=postgres;Password=123";

        // Метод для проверки существования корзины пользователя
        public static int GetCartId(int userId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT id_cart FROM cart WHERE id_user = @UserId";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    var result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }

                    // Если корзины нет, создаем новую
                    return CreateCart(userId, conn);
                }
            }
        }

        // Создание новой корзины
        private static int CreateCart(int userId, NpgsqlConnection conn)
        {
            // Проверяем, существует ли пользователь
            string checkUserQuery = "SELECT COUNT(*) FROM userwpf WHERE id_user = @UserId";
            using (var cmdCheckUser = new NpgsqlCommand(checkUserQuery, conn))
            {
                cmdCheckUser.Parameters.AddWithValue("@UserId", userId);
                long userExists = (long)cmdCheckUser.ExecuteScalar();

                if (userExists == 0)
                {
                    throw new Exception($"Пользователь с ID {userId} не существует!");
                }
            }

            // Если пользователь существует, создаем корзину
            string maxIdQuery = "SELECT COALESCE(MAX(id_cart), 0) + 1 FROM cart";
            int newCartId;

            using (var cmdMaxId = new NpgsqlCommand(maxIdQuery, conn))
            {
                newCartId = Convert.ToInt32(cmdMaxId.ExecuteScalar());
            }

            string insertQuery = @"
        INSERT INTO cart (id_cart, id_user)
        VALUES (@CartId, @UserId)
        RETURNING id_cart";

            using (var cmd = new NpgsqlCommand(insertQuery, conn))
            {
                cmd.Parameters.AddWithValue("@CartId", newCartId);
                cmd.Parameters.AddWithValue("@UserId", userId);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // Добавление товара в корзину
        public static bool AddToCart(int userId, int bookId, int quantity = 1)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    //GetCartId(userId);

                    // Проверяем, есть ли уже корзина у пользователя
                    string checkCartQuery = "SELECT id_cart FROM cart WHERE id_user = @UserId";
                    int cartId;

                    using (var cmdCheck = new NpgsqlCommand(checkCartQuery, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@UserId", userId);
                        var result = cmdCheck.ExecuteScalar();

                        if (result == null)
                        {
                            // Корзины нет, создаем новую с генерацией id_cart
                            string maxIdQuery = "SELECT COALESCE(MAX(id_cart), 0) + 1 FROM cart";
                            using (var cmdMaxId = new NpgsqlCommand(maxIdQuery, conn))
                            {
                                cartId = Convert.ToInt32(cmdMaxId.ExecuteScalar());
                            }

                            string createCartQuery = "INSERT INTO cart (id_cart, id_user) VALUES (@CartId, @UserId)";
                            using (var cmdCreate = new NpgsqlCommand(createCartQuery, conn))
                            {
                                cmdCreate.Parameters.AddWithValue("@CartId", cartId);
                                cmdCreate.Parameters.AddWithValue("@UserId", userId);
                                cmdCreate.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            cartId = Convert.ToInt32(result);
                        }
                    }

                    // Проверяем, есть ли уже такая книга в корзине
                    string checkItemQuery = "SELECT id_cart_elements, quantity_goods FROM cart_item WHERE id_cart = @CartId AND id_book = @BookId";
                    using (var cmdCheckItem = new NpgsqlCommand(checkItemQuery, conn))
                    {
                        cmdCheckItem.Parameters.AddWithValue("@CartId", cartId);
                        cmdCheckItem.Parameters.AddWithValue("@BookId", bookId);

                        using (var reader = cmdCheckItem.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Книга уже есть в корзине, обновляем количество
                                int cartItemId = reader.GetInt32(0);
                                int currentQuantity = reader.GetInt32(1);
                                reader.Close();

                                string updateQuery = "UPDATE cart_item SET quantity_goods = @Quantity WHERE id_cart_elements = @CartItemId";
                                using (var cmdUpdate = new NpgsqlCommand(updateQuery, conn))
                                {
                                    cmdUpdate.Parameters.AddWithValue("@Quantity", currentQuantity + quantity);
                                    cmdUpdate.Parameters.AddWithValue("@CartItemId", cartItemId);
                                    cmdUpdate.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                reader.Close();

                                // Генерируем новый id для cart_item
                                string maxItemIdQuery = "SELECT COALESCE(MAX(id_cart_elements), 0) + 1 FROM cart_item";
                                int newItemId;

                                using (var cmdMaxItemId = new NpgsqlCommand(maxItemIdQuery, conn))
                                {
                                    newItemId = Convert.ToInt32(cmdMaxItemId.ExecuteScalar());
                                }

                                // Книги нет в корзине, добавляем новую запись с явным указанием id_cart_elements
                                string insertQuery = @"
                            INSERT INTO cart_item (id_cart_elements, id_cart, id_book, quantity_goods)
                            VALUES (@ItemId, @CartId, @BookId, @Quantity)";

                                using (var cmdInsert = new NpgsqlCommand(insertQuery, conn))
                                {
                                    cmdInsert.Parameters.AddWithValue("@ItemId", newItemId);
                                    cmdInsert.Parameters.AddWithValue("@CartId", cartId);
                                    cmdInsert.Parameters.AddWithValue("@BookId", bookId);
                                    cmdInsert.Parameters.AddWithValue("@Quantity", quantity);
                                    cmdInsert.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении в корзину: {ex.Message}");
                return false;
            }
        }

        // Обновление количества товара в корзине
        private static bool UpdateCartItemQuantity(int cartItemId, int newQuantity, NpgsqlConnection conn)
        {
            string query = @"
            UPDATE cart_item 
            SET quantity_goods = @Quantity 
            WHERE id_cart_elements = @CartItemId";

            using (var cmd = new NpgsqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Quantity", newQuantity);
                cmd.Parameters.AddWithValue("@CartItemId", cartItemId);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Добавление нового товара в корзину
        private static bool AddNewCartItem(int cartId, int bookId, int quantity, NpgsqlConnection conn)
        {
            string query = @"
            INSERT INTO cart_item (id_cart, id_book, quantity_goods)
            VALUES (@CartId, @BookId, @Quantity)";

            using (var cmd = new NpgsqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@CartId", cartId);
                cmd.Parameters.AddWithValue("@BookId", bookId);
                cmd.Parameters.AddWithValue("@Quantity", quantity);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
       
        public static bool UpdateCartItemQuantity(int cartItemId, int newQuantity)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                UPDATE cart_item 
                SET quantity_goods = @Quantity 
                WHERE id_cart_elements = @CartItemId";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Quantity", newQuantity);
                        cmd.Parameters.AddWithValue("@CartItemId", cartItemId);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении количества: {ex.Message}");
                return false;
            }
        }
        // Получение всех товаров в корзине
        public static List<CartItemViewModel> GetCartItems(int userId)
        {
            List<CartItemViewModel> items = new List<CartItemViewModel>();
            int cartId = GetCartId(userId);

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT ci.id_cart_elements, b.id_book, b.title, b.price, ci.quantity_goods, b.cover_image
                FROM cart_item ci
                JOIN book b ON ci.id_book = b.id_book
                WHERE ci.id_cart = @CartId";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CartId", cartId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new CartItemViewModel
                            {
                                CartItemId = reader.GetInt32(0),
                                BookId = reader.GetInt32(1),
                                Title = reader.GetString(2),
                                Price = reader.GetDecimal(3),
                                Quantity = reader.GetInt32(4),
                                CoverImage = reader.IsDBNull(5) ? null : reader.GetString(5)
                            });
                        }
                    }
                }
            }

            return items;
        }

        // Удаление товара из корзины
        public static bool RemoveFromCart(int cartItemId)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM cart_item WHERE id_cart_elements = @CartItemId";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CartItemId", cartItemId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении из корзины: {ex.Message}");
                return false;
            }
        }
    }

    // Модель представления для элемента корзины
    //public class CartItemViewModel
    //{
    //    public int CartItemId { get; set; }
    //    public int BookId { get; set; }
    //    public string Title { get; set; }
    //    public decimal Price { get; set; }
    //    public int Quantity { get; set; }
    //    public string CoverImage { get; set; }
    //    public decimal TotalPrice => Price * Quantity;
    //}
}
