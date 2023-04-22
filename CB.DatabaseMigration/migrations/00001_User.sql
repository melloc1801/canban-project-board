CREATE TABLE Users (
    id INT PRIMARY KEY,
    username TEXT NOT NULL,
    email TEXT NOT NULL,
    avatarURL TEXT,
    passwordHash TEXT NOT NULL,
    refreshToken TEXT
);