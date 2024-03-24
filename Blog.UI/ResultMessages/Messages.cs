namespace Blog.UI.ResultMessages
{
    public class Messages
    {
        public static class Article
        {
            public static string Add(string articleTitle)
            {
                return $"{articleTitle} başlıklı makale başarıyla eklenmiştir.";
            }
            public static string Update(string articleTitle)
            {
                return $"{articleTitle} başlıklı makale başarıyla güncellenmiştir.";
            }
            public static string Delete(string articleTitle)
            {
                return $"{articleTitle} başlıklı makale başarıyla silinmiştir.";
            }
            public static string UndoDelete(string articleTitle)
            {
                return $"{articleTitle} başlıklı makale başarıyla geri eklenmiştir";
            }
        }

        public static class Category
        {
            public static string Add(string categoryName)
            {
                return $"{categoryName} isimli kategori başarıyla eklenmiştir.";
            }
            public static string Update(string categoryName)
            {
                return $"{categoryName} isimli kategori başarıyla güncellenmiştir.";
            }
            public static string Delete(string categoryName)
            {
                return $"{categoryName} isimli kategori başarıyla silinmiştir.";
            }
            public static string UndoDelete(string categoryName)
            {
                return $"{categoryName} isimli kategori başarıyla geri eklenmiştir.";
            }
        }
        public static class User
        {
            public static string Add(string email)
            {
                return $"{email} isimli kullanıcı başarıyla eklenmiştir.";
            }
            public static string Update(string email)
            {
                return $"{email} isimli kullanıcı başarıyla güncellenmiştir.";
            }
            public static string Delete(string email)
            {
                return $"{email} isimli kullanıcı başarıyla silinmiştir.";
            }
        }
    }
}
