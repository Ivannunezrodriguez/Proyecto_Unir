-- Crear la base de datos
CREATE DATABASE SmartGameDB;
\c SmartGameDB;

-- Crear la tabla de Usuarios
CREATE TABLE Users (
    user_id SERIAL PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    email VARCHAR(100) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    role VARCHAR(20) DEFAULT 'user',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Crear la tabla de Videojuegos (Referenciando IGDB)
CREATE TABLE Games (
    game_id SERIAL PRIMARY KEY,
    igdb_id INT NOT NULL UNIQUE  -- ID de referencia en IGDB
);

-- Crear la tabla de Calificaciones
CREATE TABLE Ratings (
    rating_id SERIAL PRIMARY KEY,
    user_id INT NOT NULL REFERENCES Users(user_id) ON DELETE CASCADE,
    game_id INT NOT NULL REFERENCES Games(game_id) ON DELETE CASCADE,
    score INT NOT NULL CHECK (score BETWEEN 1 AND 10),
    review TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Crear la tabla de Favoritos
CREATE TABLE Favorites (
    favorite_id SERIAL PRIMARY KEY,
    user_id INT NOT NULL REFERENCES Users(user_id) ON DELETE CASCADE,
    game_id INT NOT NULL REFERENCES Games(game_id) ON DELETE CASCADE,
    added_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UNIQUE (user_id, game_id)  -- Un usuario no puede agregar un juego dos veces
);

-- Crear la tabla de Recomendaciones
CREATE TABLE Recommendations (
    recommendation_id SERIAL PRIMARY KEY,
    user_id INT NOT NULL REFERENCES Users(user_id) ON DELETE CASCADE,
    game_id INT NOT NULL REFERENCES Games(game_id) ON DELETE CASCADE,
    reason TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Crear la tabla de Estado del Juego (Jugado / Deseado)
CREATE TABLE GameStatus (
    status_id SERIAL PRIMARY KEY,
    user_id INT NOT NULL REFERENCES Users(user_id) ON DELETE CASCADE,
    game_id INT NOT NULL REFERENCES Games(game_id) ON DELETE CASCADE,
    status VARCHAR(20) CHECK (status IN ('Jugado', 'Deseado')),  -- Aumentado a VARCHAR(20) por posibles expansiones futuras
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UNIQUE (user_id, game_id)  -- Evita estados duplicados para el mismo juego y usuario
);

-- Índices para mejorar la búsqueda y la eficiencia
CREATE INDEX idx_users_email ON Users(email);
CREATE INDEX idx_games_igdb ON Games(igdb_id);
CREATE INDEX idx_ratings_user ON Ratings(user_id);
CREATE INDEX idx_favorites_user ON Favorites(user_id);
CREATE INDEX idx_recommendations_user ON Recommendations(user_id);
CREATE INDEX idx_gamestatus_user_game ON GameStatus(user_id, game_id);  -- Índice compuesto para búsquedas más eficientes
