INSERT INTO users (id, username, email, password_hash, created_at)
VALUES
    (gen_random_uuid(), 'gamer1', 'gamer1@example.com', 'hashedpassword1', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'gamer2', 'gamer2@example.com', 'hashedpassword2', CURRENT_TIMESTAMP),
    (gen_random_uuid(), 'gamer3', 'gamer3@example.com', 'hashedpassword3', CURRENT_TIMESTAMP);
INSERT INTO categories (id, name)
VALUES
    (gen_random_uuid(), 'RPG'),
    (gen_random_uuid(), 'Acción'),
    (gen_random_uuid(), 'Aventura'),
    (gen_random_uuid(), 'Mundo Abierto');
INSERT INTO videogames (id, title, description, release_date, cover_url, developer, platform, rating, category_id)
VALUES
    (gen_random_uuid(), 'The Witcher 3: Wild Hunt', 'Un RPG de mundo abierto con una gran historia.', '2015-05-19', 'https://example.com/witcher3.jpg', 'CD Projekt Red', 'PC', 9.7, (SELECT id FROM categories WHERE name = 'RPG')),
    (gen_random_uuid(), 'Cyberpunk 2077', 'Juego de rol y acción ambientado en un futuro distópico.', '2020-12-10', 'https://example.com/cyberpunk2077.jpg', 'CD Projekt Red', 'PC', 8.5, (SELECT id FROM categories WHERE name = 'Acción')),
    (gen_random_uuid(), 'God of War', 'Kratos se enfrenta a los dioses nórdicos.', '2018-04-20', 'https://example.com/godofwar.jpg', 'Santa Monica Studio', 'PlayStation 4', 9.8, (SELECT id FROM categories WHERE name = 'Acción')),
    (gen_random_uuid(), 'Red Dead Redemption 2', 'Explora el Salvaje Oeste en un juego de mundo abierto.', '2018-10-26', 'https://example.com/rdr2.jpg', 'Rockstar Games', 'PC', 9.6, (SELECT id FROM categories WHERE name = 'Mundo Abierto')),
    (gen_random_uuid(), 'Elden Ring', 'Una épica aventura en un mundo de fantasía oscura.', '2022-02-25', 'https://example.com/eldenring.jpg', 'FromSoftware', 'PlayStation 5', 9.5, (SELECT id FROM categories WHERE name = 'RPG'));
INSERT INTO videogame_genres (videogame_id, genre)
VALUES
    ((SELECT id FROM videogames WHERE title = 'The Witcher 3: Wild Hunt'), 'RPG'),
    ((SELECT id FROM videogames WHERE title = 'Cyberpunk 2077'), 'RPG'),
    ((SELECT id FROM videogames WHERE title = 'God of War'), 'Acción'),
    ((SELECT id FROM videogames WHERE title = 'Red Dead Redemption 2'), 'Mundo Abierto'),
    ((SELECT id FROM videogames WHERE title = 'Elden Ring'), 'RPG');
INSERT INTO videogame_platforms (videogame_id, platform)
VALUES
    ((SELECT id FROM videogames WHERE title = 'The Witcher 3: Wild Hunt'), 'PC'),
    ((SELECT id FROM videogames WHERE title = 'Cyberpunk 2077'), 'PC'),
    ((SELECT id FROM videogames WHERE title = 'God of War'), 'PlayStation 4'),
    ((SELECT id FROM videogames WHERE title = 'Red Dead Redemption 2'), 'PC'),
    ((SELECT id FROM videogames WHERE title = 'Elden Ring'), 'PlayStation 5');
INSERT INTO played_games (id, user_id, videogame_id, played_at)
VALUES
    (gen_random_uuid(), (SELECT id FROM users WHERE username = 'gamer1'), (SELECT id FROM videogames WHERE title = 'The Witcher 3: Wild Hunt'), CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT id FROM users WHERE username = 'gamer2'), (SELECT id FROM videogames WHERE title = 'Cyberpunk 2077'), CURRENT_TIMESTAMP);
INSERT INTO reviews (id, user_id, videogame_id, rating, comment, created_at)
VALUES
    (gen_random_uuid(), (SELECT id FROM users WHERE username = 'gamer1'), (SELECT id FROM videogames WHERE title = 'The Witcher 3: Wild Hunt'), 10, 'Un juego increíble con una historia profunda.', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT id FROM users WHERE username = 'gamer2'), (SELECT id FROM videogames WHERE title = 'Cyberpunk 2077'), 8, 'Gran mundo, pero algunos bugs al inicio.', CURRENT_TIMESTAMP);
INSERT INTO purchases (id, user_id, videogame_id, purchase_date, store, price)
VALUES
    (gen_random_uuid(), (SELECT id FROM users WHERE username = 'gamer1'), (SELECT id FROM videogames WHERE title = 'The Witcher 3: Wild Hunt'), CURRENT_TIMESTAMP, 'Steam', 29.99),
    (gen_random_uuid(), (SELECT id FROM users WHERE username = 'gamer2'), (SELECT id FROM videogames WHERE title = 'Cyberpunk 2077'), CURRENT_TIMESTAMP, 'GOG', 39.99);
INSERT INTO recommendations (id, user_id, recommended_videogame_id, reason, created_at)
VALUES
    (gen_random_uuid(), (SELECT id FROM users WHERE username = 'gamer1'), (SELECT id FROM videogames WHERE title = 'Cyberpunk 2077'), 'Si te gustó The Witcher 3, probablemente disfrutes este juego.', CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT id FROM users WHERE username = 'gamer2'), (SELECT id FROM videogames WHERE title = 'Elden Ring'), 'Si te gustan los desafíos, este es un juego increíble.', CURRENT_TIMESTAMP);
INSERT INTO ai_analysis (id, user_id, videogame_id, ai_score, created_at)
VALUES
    (gen_random_uuid(), (SELECT id FROM users WHERE username = 'gamer1'), (SELECT id FROM videogames WHERE title = 'The Witcher 3: Wild Hunt'), 9.5, CURRENT_TIMESTAMP),
    (gen_random_uuid(), (SELECT id FROM users WHERE username = 'gamer2'), (SELECT id FROM videogames WHERE title = 'Cyberpunk 2077'), 8.2, CURRENT_TIMESTAMP);
