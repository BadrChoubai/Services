FROM node:20 AS build

WORKDIR /app

COPY package.json ./
RUN npm install

COPY . .

RUN npm run build

FROM node:20 AS final

WORKDIR /app

COPY --from=build /app/config.json ./config.json
COPY --from=build /app/dist ./dist

COPY package*.json ./

RUN npm install --only=production

EXPOSE 80
ENV NODE_ENV=production

CMD ["node", "dist/app.js"]

