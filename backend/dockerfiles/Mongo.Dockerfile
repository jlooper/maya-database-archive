FROM mongo:4.2.5-bionic

ENV MONGO_INITDB_ROOT_USERNAME=admin \ 
    MONGO_INITDB_ROOT_PASSWORD=admin \
    MONGO_INITDB_DATABASE=maya-db