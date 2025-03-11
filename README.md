---

# **ANTEPROYECTO**
## **Catálogo Inteligente de Videojuegos con IA y Recomendaciones Personalizadas**

### **Plataformas:** Web  
### **Tecnologías:** ASP.NET Core (C#) (Backend), Angular (TypeScript) (Frontend), PostgreSQL y MongoDB (Base de Datos), Docker (Despliegue), Oracle Cloud VPS (Servidor)  

---

## **1. INTRODUCCIÓN**
El mundo de los videojuegos cuenta con una enorme cantidad de títulos disponibles, lo que hace que encontrar un juego adecuado según los gustos del usuario sea un desafío. Este proyecto busca desarrollar una **plataforma de recomendación de videojuegos** donde los usuarios puedan **explorar, calificar y recibir sugerencias personalizadas**. Además, la plataforma integrará una **IA** que recomendará juegos en función de los intereses del usuario y proporcionará **tips y ayuda en los títulos en los que está jugando**. Para enriquecer el catálogo, también se incluirán **videos de YouTube oficiales del juego, así como gameplays con mayores visualizaciones**.

---

## **2. OBJETIVOS**
### **Objetivo General**
Desarrollar una aplicación web que permita a los usuarios explorar videojuegos por categorías, notas y filtros avanzados, con funciones de recomendación basada en IA, acceso a noticias y compras en tiendas externas, e integración de contenido multimedia.

### **Objetivos Específicos**
1. **Desarrollar una API RESTful** en ASP.NET Core (C#) que permita la consulta y filtrado de videojuegos por categorías, géneros, plataformas y puntuaciones.
2. **Implementar PostgreSQL y MongoDB** como bases de datos para almacenar información sobre los videojuegos, opiniones y preferencias de los usuarios.
3. **Implementar un sistema de autenticación** con JWT para permitir el registro e inicio de sesión de usuarios.
4. **Diseñar un sistema de puntuación y comentarios** donde los usuarios puedan evaluar los juegos jugados.
5. **Desarrollar una IA de recomendación de videojuegos** que sugiera títulos similares según el historial y preferencias del usuario.
6. **Incluir un sistema de ayuda con IA** que brinde tips y estrategias basadas en la experiencia del jugador.
7. **Integrar un feed de noticias sobre videojuegos** con información actualizada.
8. **Implementar enlaces a tiendas externas** para facilitar la compra de videojuegos desde la plataforma.
9. **Incorporar videos de YouTube oficiales y gameplays populares** dentro de la plataforma para enriquecer la experiencia del usuario.
10. **Desplegar la aplicación usando Docker en un servidor VPS de Oracle Cloud**.

---

## **3. DESCRIPCIÓN DEL PROYECTO**
### **3.1. Autenticación y Gestión de Usuarios**
- Registro e inicio de sesión con JWT.
- Perfil de usuario con juegos jugados y lista de favoritos.
- Posibilidad de modificar preferencias para mejorar las recomendaciones.

### **3.2. Exploración y Filtros Avanzados**
- Búsqueda de juegos por nombre, categoría, plataforma, desarrollador, etc.
- Filtros avanzados (nota, género, precio, lanzamiento, etc.).
- Listado de juegos más populares y mejor valorados.

### **3.3. Recomendaciones con IA**
- Algoritmo de recomendación basado en **Machine Learning**.
- Predicción de juegos que podrían gustarle al usuario según su historial.
- Posibilidad de recibir tips y ayuda en juegos que está jugando.

### **3.4. Reseñas y Valoraciones**
- Los usuarios podrán **calificar** los juegos que han jugado con una nota personal.
- Espacio para **dejar opiniones** sobre la experiencia con cada juego.
- Sistema de **puntuación media** de cada juego basado en las calificaciones de la comunidad.
- Sección con **opiniones destacadas** y posibilidad de filtrar reseñas (positivas, negativas, más recientes).
- Opción de **marcar reseñas como útiles** o **seguir a otros jugadores** con gustos similares.

### **3.5. Noticias y Actualizaciones**
- Sección de noticias sobre lanzamientos, actualizaciones y novedades del mundo gamer.
- Integración con APIs de noticias de videojuegos (IGN, Kotaku, etc.).

### **3.6. Acceso a Tiendas Externas**
- Enlace directo a la compra de juegos en plataformas como **Steam, Epic Games, PlayStation Store, Xbox Store** y más.
- Comparación de precios entre tiendas oficiales.

### **3.7. Integración de Videos de YouTube**
- Visualización de **tráilers y videos oficiales** de cada juego desde YouTube.
- Integración de **gameplays con mayor número de visualizaciones** para cada título.
- Sección de videos destacados según la comunidad.

---

## **4. TECNOLOGÍAS UTILIZADAS**
### **Backend**
- **ASP.NET Core (C#):** API RESTful para gestionar datos.
- **Entity Framework Core:** ORM para manejar la persistencia en **PostgreSQL**.
- **MongoDB:** Para almacenamiento flexible de datos no estructurados.
- **JWT (JSON Web Token):** Autenticación segura de usuarios.

### **Frontend**
- **Web:** Angular (TypeScript).

### **Infraestructura y Despliegue**
- **Docker:** Contenedorización del backend y frontend.
- **Oracle Cloud VPS:** Servidor para la implementación de la aplicación.

### **Inteligencia Artificial**
- **ML.NET o TensorFlow:** Algoritmo de recomendación de videojuegos.
- **Modelos NLP:** Análisis de reseñas y generación de tips de ayuda.

---

## **5. METODOLOGÍA**
Se utilizará **metodología ágil (Scrum/Kanban)** con entregas incrementales:
1. **Fase 1:** Análisis y diseño de la arquitectura.
2. **Fase 2:** Desarrollo del backend y API.
3. **Fase 3:** Desarrollo del frontend en Angular.
4. **Fase 4:** Implementación del sistema de IA.
5. **Fase 5:** Pruebas unitarias e integración continua.
6. **Fase 6:** Optimización y mejoras en la experiencia de usuario.
7. **Fase 7:** Despliegue en Docker sobre Oracle Cloud VPS.
8. **Fase 8:** Documentación y mantenimiento.

---

## **6. RESULTADOS ESPERADOS**
- Plataforma funcional donde los usuarios puedan encontrar, filtrar y descubrir videojuegos.
- Algoritmo de IA capaz de recomendar juegos según los gustos del usuario.
- Aplicación web con una interfaz moderna e intuitiva.
- Implementación de noticias, acceso a tiendas externas y videos de YouTube para enriquecer la experiencia del usuario.
- Despliegue de la aplicación en un entorno escalable mediante Docker en Oracle Cloud VPS.

---

## **7. CONCLUSIÓN**
Este proyecto ofrecerá a los jugadores una herramienta completa para explorar el mundo de los videojuegos, mejorando su experiencia con recomendaciones inteligentes y contenido relevante.

---