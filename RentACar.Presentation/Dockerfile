FROM node:16-alpine3.11 as build
WORKDIR /app
COPY package.json ./
RUN npm install
COPY . .

ENV REACT_APP_API_URL https://localhost:5001/api/

RUN npm run build

FROM nginx:1.19.0-alpine as prod-stage
COPY --from=build /app/build /usr/share/nginx/html
EXPOSE 80
CMD [ "nginx", "-g", "daemon off;" ]