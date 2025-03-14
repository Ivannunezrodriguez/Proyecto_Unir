-- Extensión para UUID
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Tabla de Usuarios
CREATE TABLE Users (
    id_user UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    email VARCHAR(255) UNIQUE NOT NULL,
    password TEXT NOT NULL,
    registration_date DATE DEFAULT CURRENT_DATE
);

-- Tabla de Categorías de Videojuegos
CREATE TABLE Category (
    id_category UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
    name VARCHAR(255) UNIQUE NOT NULL
);

-- Tabla de Videojuegos
CREATE TABLE VideoGame (
    id_videogame UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    description TEXT,
    release_date DATE,
    developer VARCHAR(255),
    platform VARCHAR(100),
    rating FLOAT CHECK (rating >= 0 AND rating <= 10),
    id_category UUID,
    FOREIGN KEY (id_category) REFERENCES Category(id_category) ON DELETE SET NULL
);

-- Tabla de Compras de Videojuegos
CREATE TABLE Purchase (
    id_purchase UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
    id_user UUID NOT NULL,
    id_videogame UUID NOT NULL,
    store VARCHAR(255),
    price FLOAT CHECK (price >= 0),
    link TEXT,
    FOREIGN KEY (id_user) REFERENCES Users(id_user) ON DELETE CASCADE,
    FOREIGN KEY (id_videogame) REFERENCES VideoGame(id_videogame) ON DELETE CASCADE
);

-- Tabla de Reseñas de Videojuegos
CREATE TABLE Review (
    id_review UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
    id_user UUID NOT NULL,
    id_videogame UUID NOT NULL,
    comment TEXT,
    rating FLOAT CHECK (rating >= 0 AND rating <= 10),
    date DATE DEFAULT CURRENT_DATE,
    FOREIGN KEY (id_user) REFERENCES Users(id_user) ON DELETE CASCADE,
    FOREIGN KEY (id_videogame) REFERENCES VideoGame(id_videogame) ON DELETE CASCADE
);

-- Tabla de Recomendaciones (IA)
CREATE TABLE Recommendation (
    id_recommendation UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
    id_user UUID NOT NULL,
    id_videogame UUID NOT NULL,
    reason TEXT,
    date DATE DEFAULT CURRENT_DATE,
    FOREIGN KEY (id_user) REFERENCES Users(id_user) ON DELETE CASCADE,
    FOREIGN KEY (id_videogame) REFERENCES VideoGame(id_videogame) ON DELETE CASCADE
);

-- Tabla de Análisis de IA sobre videojuegos
CREATE TABLE AI_Analysis (
    id_analysis UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
    id_user UUID NOT NULL,
    id_videogame UUID NOT NULL,
    ai_score FLOAT CHECK (ai_score >= 0 AND ai_score <= 10),
    generated_at DATE DEFAULT CURRENT_DATE,
    FOREIGN KEY (id_user) REFERENCES Users(id_user) ON DELETE CASCADE,
    FOREIGN KEY (id_videogame) REFERENCES VideoGame(id_videogame) ON DELETE CASCADE
);
