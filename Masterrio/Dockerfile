# Layihəni build (yığmaq) üçün Node.js-in 20-ci versiyasından istifadə edirik
FROM node:20-alpine AS build
# Konteyner daxilində iş qovluğunu təyin edirik
WORKDIR /app

# package.json və package-lock.json fayllarını kopyalayırıq
COPY package*.json ./
# Lazım olan paketləri (dependency) yükləyirik
RUN npm install

# Bütün layihə fayllarını kopyalayırıq
COPY . .
# Layihəni istehsalata (production) hazırlamaq üçün build edirik
RUN npm run build

# Son mərhələ - saytı yayınlamaq üçün Nginx-dən istifadə edirik
FROM nginx:alpine AS final
# Build mərhələsində yaranan static faylları (dist qovluğunu) Nginx-in default qovluğuna kopyalayırıq
COPY --from=build /app/dist /usr/share/nginx/html
# Xüsusi Nginx konfiqurasiyasını kopyalayırıq (Proxy üçün)
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Standart web portu olan 80-i kənar istifadə üçün açırıq
EXPOSE 80

# Nginx-i arxa planda deyil, əsas proses kimi (foreground) işə salırıq
CMD ["nginx", "-g", "daemon off;"]
