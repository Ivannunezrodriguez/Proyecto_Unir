-- 🟢 Insertar Usuarios
INSERT INTO Users (Username, Email, Password, Role) VALUES
('gamer1', 'gamer1@example.com', 'hashed_password', 'user'),
('gamer2', 'gamer2@example.com', 'hashed_password', 'user'),
('gamer3', 'gamer3@example.com', 'hashed_password', 'user'),
('admin', 'admin@example.com', 'hashed_password', 'admin');

-- 🟢 Insertar Juegos de IGDB
INSERT INTO Games (IgdbId, GameTitle) VALUES
(1942, 'The Witcher 3'),
(732, 'Cyberpunk 2077'),
(1053, 'Halo Infinite'),
(827, 'Elden Ring'),
(909, 'Red Dead Redemption 2');

-- 🟢 Insertar Calificaciones Aleatorias
INSERT INTO Ratings (UserId, GameId, Score, Review) VALUES
(1, 1, 10, 'Increíble historia y jugabilidad.'),
(2, 2, 8, 'Gráficos impresionantes, pero algunos bugs.'),
(3, 3, 9, 'Excelente shooter con gran historia.'),
(1, 4, 10, 'Mundo abierto impresionante.'),
(2, 5, 9, 'Historia cinematográfica y realista.');

-- 🟢 Insertar Estados de Juegos
INSERT INTO GameStatuses (UserId, GameId, Status) VALUES
(1, 1, 'Completed'),
(2, 2, 'Playing'),
(3, 3, 'Wishlist'),
(1, 4, 'Owned'),
(2, 5, 'Abandoned');

-- 🟢 Insertar Favoritos
INSERT INTO Favorites (UserId, GameId) VALUES
(1, 1),
(2, 3),
(3, 5),
(1, 4),
(2, 2);

-- 🟢 Insertar Recomendaciones Basadas en Weaviate
INSERT INTO Recommendations (UserId, GameId, IgdbId, GameTitle, Reason) VALUES
(1, 4, 1942, 'The Witcher 3', 'Gran historia y mundo abierto'),
(2, 3, 1053, 'Halo Infinite', 'Si te gustan los shooters, este es ideal'),
(3, 5, 909, 'Red Dead Redemption 2', 'Exploración y narrativa al máximo'),
(1, 2, 732, 'Cyberpunk 2077', 'Mundo futurista y libertad total'),
(2, 1, 827, 'Elden Ring', 'Desafío y exploración sin igual.');
