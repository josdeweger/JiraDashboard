FROM node:7.2.1

WORKDIR /var/www/jira-node-server

RUN npm install -g pm2@latest

RUN mkdir -p /var/log/pm2

COPY . .

RUN npm install

ENTRYPOINT pm2 start pm2.json --env development --no-daemon

EXPOSE 3001
