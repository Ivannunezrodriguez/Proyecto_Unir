-- Crear la base de datos
CREATE DATABASE SmartGameDB;
\c SmartGameDB;

-- Crear la tabla de Usuarios
CREATE TABLE Users (
    UserId SERIAL PRIMARY KEY,
    Username VARCHAR(100) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Role VARCHAR(20) DEFAULT 'user',
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Crear la tabla de Videojuegos (Referenciando IGDB)
CREATE TABLE Games (
    GameId SERIAL PRIMARY KEY,
    IgdbId INT NOT NULL UNIQUE
);

-- Crear la tabla de Calificaciones
CREATE TABLE Ratings (
    RatingId SERIAL PRIMARY KEY,
    UserId INT NOT NULL,
    GameId INT NOT NULL,
    Score INT NOT NULL CHECK (Score >= 1 AND Score <= 10),
    Review VARCHAR(500),
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE,
    FOREIGN KEY (GameId) REFERENCES Games(GameId) ON DELETE CASCADE
);

-- Crear la tabla de Favoritos
CREATE TABLE Favorites (
    FavoriteId SERIAL PRIMARY KEY,
    UserId INT NOT NULL,
    GameId INT NOT NULL,
    AddedAt TIMESTAMP DEFAULT NOW(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE,
    FOREIGN KEY (GameId) REFERENCES Games(GameId) ON DELETE CASCADE
);

-- Crear la tabla de Recomendaciones
CREATE TABLE Recommendations (
    RecommendationId SERIAL PRIMARY KEY,
    UserId INT NOT NULL,
    GameId INT NOT NULL,
    Reason TEXT NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE,
    FOREIGN KEY (GameId) REFERENCES Games(GameId) ON DELETE CASCADE
);

-- Crear la tabla de Estado del Juego (Jugado / Deseado)
CREATE TABLE GameStatuses (
    StatusId SERIAL PRIMARY KEY,
    UserId INT NOT NULL,
    GameId INT NOT NULL,
    Status VARCHAR(10) NOT NULL CHECK (Status IN ('Jugado', 'Deseado')),
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE,
    FOREIGN KEY (GameId) REFERENCES Games(GameId) ON DELETE CASCADE
);

-- Índices para mejorar la búsqueda y la eficiencia
CREATE INDEX idx_users_email ON Users(Email);
CREATE INDEX idx_games_igdb ON Games(IgdbId);
CREATE INDEX idx_ratings_user ON Ratings(UserId);
CREATE INDEX idx_ratings_game ON Ratings(GameId);
CREATE INDEX idx_favorites_user ON Favorites(UserId);
CREATE INDEX idx_favorites_game ON Favorites(GameId);
CREATE INDEX idx_recommendations_user ON Recommendations(UserId);
CREATE INDEX idx_recommendations_game ON Recommendations(GameId);
CREATE INDEX idx_gamestatus_user_game ON GameStatuses(UserId, GameId);  -- Índice compuesto para búsquedas más eficientes
