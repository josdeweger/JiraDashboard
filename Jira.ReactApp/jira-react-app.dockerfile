FROM node:7.2.1

ARG REACT_APP_TEAM_NAME
ARG REACT_APP_JIRA_NODE_SERVER_SETTINGS_URL
ARG REACT_APP_JIRA_API_CUSTOMER_STATUS_URL

ENV REACT_APP_TEAM_NAME ${REACT_APP_TEAM_NAME}
ENV REACT_APP_JIRA_NODE_SERVER_SETTINGS_URL ${REACT_APP_JIRA_NODE_SERVER_SETTINGS_URL}
ENV REACT_APP_JIRA_API_CUSTOMER_STATUS_URL ${REACT_APP_JIRA_API_CUSTOMER_STATUS_URL}

WORKDIR /var/www/jira-react-app

RUN npm install -g pm2@latest
RUN mkdir -p /var/log/pm2

COPY . .

RUN cd /var/www/jira-react-app && ls

WORKDIR /var/www/jira-react-app

RUN npm install
RUN chmod a+x /var/www/jira-react-app/node_modules/.bin/react-scripts
RUN npm run build

ENTRYPOINT pm2 start pm2.json --env development --no-daemon

EXPOSE 3000
