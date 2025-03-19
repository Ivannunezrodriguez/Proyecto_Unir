-- Insertar Usuarios
INSERT INTO Users (Username, Email, Password, Role) VALUES
('JuanPerez', 'juan@example.com', 'hashed_password_1', 'user'),
('MariaGomez', 'maria@example.com', 'hashed_password_2', 'admin'),
('CarlosLopez', 'carlos@example.com', 'hashed_password_3', 'user');

-- Insertar Juegos
INSERT INTO Games (IgdbId) VALUES
(1001), (1002), (1003), (1004), (1005);

-- Insertar Calificaciones
INSERT INTO Ratings (UserId, GameId, Score, Review) VALUES
(1, 1, 9, 'Gran juego, me encantó la historia'),
(2, 2, 8, 'Gráficos impresionantes'),
(3, 3, 7, 'Buen gameplay pero con fallos');

-- Insertar Favoritos
INSERT INTO Favorites (UserId, GameId) VALUES
(1, 2), (2, 3), (3, 1);

-- Insertar Recomendaciones
INSERT INTO Recommendations (UserId, GameId, Reason) VALUES
(1, 3, 'Basado en tus juegos favoritos'),
(2, 4, 'Recomendado por jugadores similares');

-- Insertar Estados de Juego
INSERT INTO GameStatuses (UserId, GameId, Status) VALUES
(1, 1, 'Jugado'),
(2, 2, 'Deseado'),
(3, 3, 'Jugado');
