-- Create Users table
CREATE TABLE Users (
    Id SERIAL PRIMARY KEY,
    Username VARCHAR(100) NOT NULL UNIQUE,
    Email VARCHAR(255) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    FirstName VARCHAR(100),
    LastName VARCHAR(100),
    CreatedAt TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    LastLoginAt TIMESTAMP,
    IsActive BOOLEAN NOT NULL DEFAULT true
);

-- Create JewelryBoxes table
CREATE TABLE JewelryBoxes (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Description VARCHAR(500),
    CreatedAt TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP,
    UserId INTEGER NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

-- Create JewelryItems table
CREATE TABLE JewelryItems (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Description VARCHAR(500),
    Type INTEGER NOT NULL, -- JewelryType enum
    Material INTEGER NOT NULL, -- JewelryMaterial enum
    Brand VARCHAR(50),
    PurchasePrice DECIMAL(10,2),
    PurchaseDate DATE,
    Location VARCHAR(100),
    IsFavorite BOOLEAN NOT NULL DEFAULT false,
    CreatedAt TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP,
    UserId INTEGER NOT NULL,
    JewelryBoxId INTEGER,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    FOREIGN KEY (JewelryBoxId) REFERENCES JewelryBoxes(Id) ON DELETE SET NULL
);

-- Create indexes for better performance
CREATE INDEX IX_Users_Email ON Users(Email);
CREATE INDEX IX_Users_Username ON Users(Username);
CREATE INDEX IX_Users_IsActive ON Users(IsActive);
CREATE INDEX IX_JewelryBoxes_UserId ON JewelryBoxes(UserId);
CREATE INDEX IX_JewelryItems_UserId ON JewelryItems(UserId);
CREATE INDEX IX_JewelryItems_JewelryBoxId ON JewelryItems(JewelryBoxId);
