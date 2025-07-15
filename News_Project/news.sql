create database News_Project1
use News_Project1 
-- Bảng Users (Người dùng)
CREATE TABLE Users (
    user_id INT IDENTITY(1,1) PRIMARY KEY,
    username VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    role VARCHAR(50) NOT NULL,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE()
);

-- Bảng Categories (Danh mục)
CREATE TABLE Categories (
    category_id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    description TEXT,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE()
);

-- Bảng Articles (Bài viết)
CREATE TABLE Articles (
    article_id INT IDENTITY(1,1) PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    content TEXT NOT NULL,
    summary TEXT,
    author_id INT,
    category_id INT,
    published_at DATETIME,
    updated_at DATETIME DEFAULT GETDATE(),
    status VARCHAR(50) DEFAULT 'draft',
    views INT DEFAULT 0,
    FOREIGN KEY (author_id) REFERENCES Users(user_id),
    FOREIGN KEY (category_id) REFERENCES Categories(category_id)
);

-- Bảng Tags (Thẻ)
CREATE TABLE Tags (
    tag_id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE()
);

-- Bảng Article_Tags (Bài viết - Thẻ)
CREATE TABLE Article_Tags (
    article_id INT,
    tag_id INT,
    created_at DATETIME DEFAULT GETDATE(),
    PRIMARY KEY (article_id, tag_id),
    FOREIGN KEY (article_id) REFERENCES Articles(article_id),
    FOREIGN KEY (tag_id) REFERENCES Tags(tag_id)
);

-- Bảng Comments (Bình luận)
CREATE TABLE Comments (
    comment_id INT IDENTITY(1,1) PRIMARY KEY,
    article_id INT,
    user_id INT,
    content TEXT NOT NULL,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (article_id) REFERENCES Articles(article_id),
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);

-- Bảng Media (Tài nguyên media)
CREATE TABLE Media (
    media_id INT IDENTITY(1,1) PRIMARY KEY,
    url VARCHAR(255) NOT NULL,
    type VARCHAR(50) NOT NULL,
    article_id INT,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (article_id) REFERENCES Articles(article_id)
);

-- Bảng Activity Logs (Lịch sử hoạt động)
CREATE TABLE Activity_Logs (
    log_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT,
    action TEXT NOT NULL,
    timestamp DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);


-- Thêm dữ liệu vào bảng Users
INSERT INTO Users (username, email, password_hash, role)
VALUES 
('admin', 'admin@example.com', 'hashedpassword123', 'Admin'),
('editor', 'editor@example.com', 'hashedpassword456', 'Editor'),
('viewer', 'viewer@example.com', 'hashedpassword789', 'Viewer');

-- Thêm dữ liệu vào bảng Categories
INSERT INTO Categories (name, description)
VALUES 
('Technology', 'Tin tức về công nghệ'),
('Politics', 'Tin tức về chính trị'),
('Sports', 'Tin tức thể thao'),
('Entertainment', 'Tin tức giải trí');

-- Thêm dữ liệu vào bảng Articles
INSERT INTO Articles (title, content, summary, author_id, category_id, published_at, status, views)
VALUES 
('Công nghệ AI trong tương lai', 'Bài viết về sự phát triển của công nghệ AI...', 'AI là gì và tầm quan trọng trong tương lai...', 1, 1, '2025-07-15 08:00:00', 'published', 100),
('Cuộc bầu cử tổng thống 2025', 'Thông tin chi tiết về bầu cử tổng thống Mỹ 2025...', 'Cuộc bầu cử tổng thống sắp tới...', 2, 2, '2025-07-14 10:00:00', 'published', 150),
('Tuyển thủ Messi ghi bàn', 'Messi ghi bàn trong trận đấu cuối cùng...', 'Một chiến thắng ấn tượng của Messi...', 2, 3, '2025-07-13 20:00:00', 'published', 200);

-- Thêm dữ liệu vào bảng Tags
INSERT INTO Tags (name)
VALUES 
('AI'),
('Politics'),
('Sports'),
('Entertainment');

-- Thêm dữ liệu vào bảng Article_Tags
INSERT INTO Article_Tags (article_id, tag_id)
VALUES 
(1, 1), 
(2, 2), 
(3, 3);

-- Thêm dữ liệu vào bảng Comments
INSERT INTO Comments (article_id, user_id, content)
VALUES 
(1, 3, 'Bài viết rất hay, rất mong chờ sự phát triển của AI!'),
(2, 1, 'Rất thú vị, tôi sẽ theo dõi sát sao bầu cử lần này.'),
(3, 2, 'Messi quả thực là một huyền thoại.');

-- Thêm dữ liệu vào bảng Media
INSERT INTO Media (url, type, article_id)
VALUES 
('https://example.com/images/ai_future.jpg', 'image', 1),
('https://example.com/images/election2025.jpg', 'image', 2),
('https://example.com/images/messi_goal.jpg', 'image', 3);

-- Thêm dữ liệu vào bảng Activity_Logs
INSERT INTO Activity_Logs (user_id, action)
VALUES 
(1, 'Đăng bài viết "Công nghệ AI trong tương lai"'),
(2, 'Chỉnh sửa bài viết "Cuộc bầu cử tổng thống 2025"'),
(3, 'Bình luận bài viết "Tuyển thủ Messi ghi bàn"');
