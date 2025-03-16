-- 📌 Insertar Usuarios Aleatorios
INSERT INTO users (username, email, password_hash) VALUES
('ivan', 'ivan@example.com', 'hashed_password1'),
('maria', 'maria@example.com', 'hashed_password2'),
('carlos', 'carlos@example.com', 'hashed_password3'),
('laura', 'laura@example.com', 'hashed_password4'),
('pedro', 'pedro@example.com', 'hashed_password5');

-- 📌 Insertar Videojuegos Aleatorios
INSERT INTO videogames (title, description, genres, platforms, release_date, cover_url) VALUES
('The Witcher 3', 'An open-world RPG set in a medieval fantasy universe.', ARRAY['RPG', 'Adventure'], ARRAY['PC', 'PS5', 'Xbox'], '2015-05-19', 'https://example.com/witcher3.jpg'),
('Cyberpunk 2077', 'An open-world RPG set in Night City.', ARRAY['RPG', 'Action'], ARRAY['PC', 'PS5', 'Xbox'], '2020-12-10', 'https://example.com/cyberpunk.jpg'),
('Dark Souls 3', 'A dark fantasy action RPG.', ARRAY['RPG', 'Action'], ARRAY['PC', 'PS4', 'Xbox'], '2016-04-12', 'https://example.com/darksouls3.jpg'),
('Horizon Zero Dawn', 'An open-world action RPG with robotic creatures.', ARRAY['RPG', 'Action'], ARRAY['PC', 'PS5'], '2017-02-28', 'https://example.com/horizon.jpg'),
('Red Dead Redemption 2', 'A western open-world adventure game.', ARRAY['Adventure', 'Action'], ARRAY['PC', 'PS5', 'Xbox'], '2018-10-26', 'https://example.com/rdr2.jpg'),
('Elden Ring', 'An open-world action RPG developed by FromSoftware.', ARRAY['RPG', 'Action'], ARRAY['PC', 'PS5', 'Xbox'], '2022-02-25', 'https://example.com/eldenring.jpg'),
('GTA V', 'An open-world crime adventure game.', ARRAY['Action', 'Adventure'], ARRAY['PC', 'PS5', 'Xbox'], '2013-09-17', 'https://example.com/gtav.jpg'),
('God of War', 'A third-person action RPG based on Norse mythology.', ARRAY['Action', 'RPG'], ARRAY['PS5', 'PC'], '2018-04-20', 'https://example.com/gow.jpg');

-- 📌 Insertar Juegos Jugados Aleatoriamente por Usuarios
INSERT INTO played_games (user_id, game_id, played_at)
VALUES
((SELECT id FROM users WHERE username='ivan'), (SELECT id FROM videogames WHERE title='The Witcher 3'), NOW() - INTERVAL '10 days'),
((SELECT id FROM users WHERE username='maria'), (SELECT id FROM videogames WHERE title='Cyberpunk 2077'), NOW() - INTERVAL '5 days'),
((SELECT id FROM users WHERE username='carlos'), (SELECT id FROM videogames WHERE title='Dark Souls 3'), NOW() - INTERVAL '15 days'),
((SELECT id FROM users WHERE username='laura'), (SELECT id FROM videogames WHERE title='Horizon Zero Dawn'), NOW() - INTERVAL '3 days'),
((SELECT id FROM users WHERE username='pedro'), (SELECT id FROM videogames WHERE title='Red Dead Redemption 2'), NOW() - INTERVAL '20 days');

-- 📌 Insertar Compras Aleatorias
INSERT INTO purchases (user_id, game_id, purchase_date)
VALUES
((SELECT id FROM users WHERE username='ivan'), (SELECT id FROM videogames WHERE title='Cyberpunk 2077'), NOW() - INTERVAL '8 days'),
((SELECT id FROM users WHERE username='maria'), (SELECT id FROM videogames WHERE title='Elden Ring'), NOW() - INTERVAL '12 days'),
((SELECT id FROM users WHERE username='carlos'), (SELECT id FROM videogames WHERE title='GTA V'), NOW() - INTERVAL '2 days'),
((SELECT id FROM users WHERE username='laura'), (SELECT id FROM videogames WHERE title='God of War'), NOW() - INTERVAL '6 days'),
((SELECT id FROM users WHERE username='pedro'), (SELECT id FROM videogames WHERE title='The Witcher 3'), NOW() - INTERVAL '30 days');

-- 📌 Insertar Reseñas Aleatorias
INSERT INTO reviews (user_id, game_id, rating, comment, created_at)
VALUES
((SELECT id FROM users WHERE username='ivan'), (SELECT id FROM videogames WHERE title='The Witcher 3'), 9, 'Increíble RPG con una historia impresionante.', NOW() - INTERVAL '7 days'),
((SELECT id FROM users WHERE username='maria'), (SELECT id FROM videogames WHERE title='Cyberpunk 2077'), 8, 'Buen juego pero con algunos bugs.', NOW() - INTERVAL '6 days'),
((SELECT id FROM users WHERE username='carlos'), (SELECT id FROM videogames WHERE title='Dark Souls 3'), 10, 'El mejor Soulsborne hasta la fecha.', NOW() - INTERVAL '15 days'),
((SELECT id FROM users WHERE username='laura'), (SELECT id FROM videogames WHERE title='Horizon Zero Dawn'), 9, 'Mundo increíble y combate divertido.', NOW() - INTERVAL '3 days'),
((SELECT id FROM users WHERE username='pedro'), (SELECT id FROM videogames WHERE title='Red Dead Redemption 2'), 10, 'Obra maestra del western.', NOW() - INTERVAL '20 days');

-- 📌 Insertar Recomendaciones Basadas en Juegos Jugados
INSERT INTO recommendations (user_id, recommended_game_id, reason, created_at)
VALUES
((SELECT id FROM users WHERE username='ivan'), (SELECT id FROM videogames WHERE title='Elden Ring'), 'Te gustan los RPGs de mundo abierto.', NOW()),
((SELECT id FROM users WHERE username='maria'), (SELECT id FROM videogames WHERE title='Horizon Zero Dawn'), 'Si te gustó Cyberpunk 2077, prueba este otro juego de mundo abierto.', NOW()),
((SELECT id FROM users WHERE username='carlos'), (SELECT id FROM videogames WHERE title='God of War'), 'Acción épica y gran narrativa.', NOW()),
((SELECT id FROM users WHERE username='laura'), (SELECT id FROM videogames WHERE title='GTA V'), 'Si te gustan los juegos de mundo abierto, este es un clásico.', NOW()),
((SELECT id FROM users WHERE username='pedro'), (SELECT id FROM videogames WHERE title='Dark Souls 3'), 'Te gustan los retos difíciles, prueba este juego.', NOW());
