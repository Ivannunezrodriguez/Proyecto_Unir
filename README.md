## Anteproyecto - Catálogo Inteligente de Videojuegos con IA y Recomendaciones Personalizadas

### **Plataformas**
- Web

### **Tecnologías**
- **Backend:** ASP.NET Core (C#), Entity Framework Core
- **Frontend:** Angular (TypeScript)
- **Bases de Datos:** PostgreSQL y MongoDB
- **Despliegue:** Docker en Oracle Cloud VPS
- **Inteligencia Artificial:** ML.NET o TensorFlow para recomendaciones, Modelos NLP para análisis de reseñas

## 1. INTRODUCCIÓN
El mundo de los videojuegos cuenta con una enorme cantidad de títulos disponibles, lo que hace que encontrar un juego adecuado según los gustos del usuario sea un desafío. Este proyecto busca desarrollar una plataforma de recomendación de videojuegos donde los usuarios puedan explorar, calificar y recibir sugerencias personalizadas. Además, la plataforma integrará una IA que recomendará juegos en función de los intereses del usuario y proporcionará tips y ayuda en los títulos en los que está jugando. Para enriquecer el catálogo, también se incluirán videos de YouTube oficiales del juego, así como gameplays con mayores visualizaciones.

## 2. OBJETIVOS
### **Objetivo General**
Desarrollar una aplicación web que permita a los usuarios explorar videojuegos por categorías, notas y filtros avanzados, con funciones de recomendación basada en IA, acceso a noticias y compras en tiendas externas, e integración de contenido multimedia.

### **Objetivos Específicos**
1. Desarrollar una API RESTful en ASP.NET Core (C#) que permita la consulta y filtrado de videojuegos por categorías, géneros, plataformas y puntuaciones.
2. Implementar PostgreSQL y MongoDB como bases de datos para almacenar información sobre los videojuegos, opiniones y preferencias de los usuarios.
3. Implementar un sistema de autenticación con JWT para permitir el registro e inicio de sesión de usuarios.
4. Diseñar un sistema de puntuación y comentarios donde los usuarios puedan evaluar los juegos jugados.
5. Desarrollar una IA de recomendación de videojuegos que sugiera títulos similares según el historial y preferencias del usuario.
6. Incluir un sistema de ayuda con IA que brinde tips y estrategias basadas en la experiencia del jugador.
7. Integrar un feed de noticias sobre videojuegos con información actualizada.
8. Implementar enlaces a tiendas externas para facilitar la compra de videojuegos desde la plataforma.
9. Incorporar videos de YouTube oficiales y gameplays populares dentro de la plataforma para enriquecer la experiencia del usuario.
10. Desplegar la aplicación usando Docker en un servidor VPS de Oracle Cloud.

## 3. DESCRIPCIÓN DEL PROYECTO
### 3.1. Autenticación y Gestión de Usuarios
- Registro e inicio de sesión con JWT.
- Perfil de usuario con juegos jugados y lista de favoritos.
- Posibilidad de modificar preferencias para mejorar las recomendaciones.

### 3.2. Exploración y Filtros Avanzados
- Búsqueda de juegos por nombre, categoría, plataforma, desarrollador, etc.
- Filtros avanzados (nota, género, precio, lanzamiento, etc.).
- Listado de juegos más populares y mejor valorados.

### 3.3. Recomendaciones con IA
- Algoritmo de recomendación basado en Machine Learning.
- Predicción de juegos que podrían gustarle al usuario según su historial.
- Posibilidad de recibir tips y ayuda en juegos que está jugando.

### 3.4. Reseñas y Valoraciones
- Los usuarios podrán calificar los juegos que han jugado con una nota personal.
- Espacio para dejar opiniones sobre la experiencia con cada juego.
- Sistema de puntuación media de cada juego basado en las calificaciones de la comunidad.
- Sección con opiniones destacadas y posibilidad de filtrar reseñas (positivas, negativas, más recientes).
- Opción de marcar reseñas como útiles o seguir a otros jugadores con gustos similares.

### 3.5. Noticias y Actualizaciones
- Sección de noticias sobre lanzamientos, actualizaciones y novedades del mundo gamer.
- Integración con APIs de noticias de videojuegos (IGN, Kotaku, etc.).

### 3.6. Acceso a Tiendas Externas
- Enlace directo a la compra de juegos en plataformas como Steam, Epic Games, PlayStation Store, Xbox Store y más.
- Comparación de precios entre tiendas oficiales.

### 3.7. Integración de Videos de YouTube
- Visualización de tráilers y videos oficiales de cada juego desde YouTube.
- Integración de gameplays con mayor número de visualizaciones para cada título.
- Sección de videos destacados según la comunidad.

## 4. INFRAESTRUCTURA DEL PROYECTO
Para garantizar un despliegue eficiente, seguro y escalable del catálogo de videojuegos, la infraestructura se basará en un **VPS con Ubuntu Server LTS** y se compondrá de múltiples contenedores gestionados con **Docker y Docker Compose**. La arquitectura propuesta incluye los siguientes componentes:

### **4.1. Servidor VPS**
- Ubuntu Server LTS.
- Firewall configurado para restringir accesos innecesarios.
- Acceso remoto seguro mediante **SSH**.

### **4.2. Contenedores y Servicios**
Se utilizará **Docker Compose** para la gestión de los siguientes servicios:

#### **Frontend**
- Aplicación Angular servida mediante **Node.js** en un contenedor.

#### **Backend**
- API REST desarrollada en **ASP.NET Core**.
- Base de datos relacional **PostgreSQL**.
- Base de datos NoSQL **MongoDB** para almacenar ciertos tipos de datos flexibles.
- **Weaviate** para potenciar búsquedas vectoriales y mejorar recomendaciones.
- Servidor web **Nginx** para gestionar las peticiones al backend.

#### **Load Balancer y Reverse Proxy**
- **Nginx** se utilizará como balanceador de carga y proxy inverso para distribuir las solicitudes correctamente entre los servicios.

#### **Integración con APIs externas**
- Conexión con la API de **IGDB** para obtener información de videojuegos y enriquecer el catálogo.

### **4.3. Seguridad y Administración**
- Restricción de acceso mediante **firewall**.
- Uso de **JWT (JSON Web Tokens)** para autenticación de usuarios.
- Implementación de **roles y permisos** en la plataforma.

### **4.4. Monitorización y Logs**
- **Prometheus + Grafana** o **ELK Stack (Elasticsearch, Logstash, Kibana)** para la monitorización del sistema y gestión de logs.

### **4.5. Despliegue y Automatización**
- **CI/CD con GitHub Actions o GitLab CI/CD** para automatizar el despliegue.
- **Docker Swarm o Kubernetes** como opción futura para escalabilidad.

## 5. RESULTADOS ESPERADOS
- Plataforma funcional donde los usuarios puedan encontrar, filtrar y descubrir videojuegos.
- Algoritmo de IA capaz de recomendar juegos según los gustos del usuario.
- Aplicación web con una interfaz moderna e intuitiva.
- Implementación de noticias, acceso a tiendas externas y videos de YouTube para enriquecer la experiencia del usuario.
- Despliegue de la aplicación en un entorno escalable mediante Docker en Oracle Cloud VPS.

## 6. CONCLUSIÓN
Este proyecto ofrecerá a los jugadores una herramienta completa para explorar el mundo de los videojuegos, mejorando su experiencia con recomendaciones inteligentes y contenido relevante. La implementación de inteligencia artificial permitirá sugerencias personalizadas y la integración con múltiples fuentes garantizará una experiencia enriquecida para los usuarios.

