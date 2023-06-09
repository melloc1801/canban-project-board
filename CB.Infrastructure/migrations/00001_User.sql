CREATE TABLE "User" (
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    email VARCHAR(320) NOT NULL UNIQUE,
    avatarURL TEXT,
    username VARCHAR(32) NOT NULL UNIQUE,
    firstname VARCHAR(50) NOT NULL,
    lastname VARCHAR(50) NOT NULL,
    passwordHash VARCHAR(64) NOT NULL,
    refreshToken TEXT
);