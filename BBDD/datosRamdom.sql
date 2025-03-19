-- Insertar Usuarios
INSERT INTO Users (username, email, password, role) VALUES
('gamer123', 'gamer123@example.com', 'hashed_password_1', 'user'),
('proplayer', 'proplayer@example.com', 'hashed_password_2', 'user'),
('casualgamer', 'casualgamer@example.com', 'hashed_password_3', 'user'),
('adminuser', 'admin@example.com', 'hashed_password_4', 'admin'),
('retrogamer', 'retro@example.com', 'hashed_password_5', 'user');

-- Insertar Juegos (Referenciando IGDB)
INSERT INTO Games (igdb_id) VALUES
(1021), (2054), (3098), (4120), (5241),
(6352), (7893), (8654), (9456), (10234);

-- Insertar Calificaciones
INSERT INTO Ratings (user_id, game_id, score, review) VALUES
(1, 1, 9, 'Increíble historia y gráficos.'),
(2, 2, 8, 'Jugabilidad sólida pero historia corta.'),
(3, 3, 7, 'Buen juego pero mecánicas repetitivas.'),
(4, 4, 10, 'Obra maestra del género RPG.'),
(5, 5, 6, 'Esperaba más contenido en el lanzamiento.'),
(1, 6, 9, 'Muy divertido y con buena rejugabilidad.'),
(2, 7, 7, 'Gráficos geniales pero IA mejorable.'),
(3, 8, 8, 'Multijugador excelente, campaña floja.'),
(4, 9, 5, 'No cumplió mis expectativas.'),
(5, 10, 10, 'El mejor juego de la saga hasta ahora.');

-- Insertar Favoritos (Evita duplicados con UNIQUE(user_id, game_id))
INSERT INTO Favorites (user_id, game_id) VALUES
(1, 2), (1, 4), (2, 1), (2, 3), (3, 5),
(3, 7), (4, 6), (4, 8), (5, 9), (5, 10);

-- Insertar Recomendaciones de la IA
INSERT INTO Recommendations (user_id, game_id, reason) VALUES
(1, 5, 'Basado en tus calificaciones altas en juegos RPG.'),
(2, 6, 'Juego similar a los que has calificado bien.'),
(3, 8, 'Recomendado por jugadores con gustos similares.'),
(4, 9, 'Coincide con tu historial de juegos favoritos.'),
(5, 2, 'Juego con mecánicas que suelen gustarte.');

-- Insertar Estado del Juego (Jugado / Deseado)
INSERT INTO GameStatus (user_id, game_id, status) VALUES
(1, 3, 'Jugado'),
(2, 5, 'Deseado'),
(3, 7, 'Jugado'),
(4, 9, 'Deseado'),
(5, 1, 'Jugado'),
(1, 6, 'Deseado'),
(2, 8, 'Jugado'),
(3, 10, 'Deseado'),
(4, 2, 'Jugado'),
(5, 4, 'Deseado');
