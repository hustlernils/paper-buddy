# How to run a script against PostgreSQL in docker

```sh
docker exec -i <container-name> psql -U <username> < <path-to-script.sql>
```

**Example:**

```sh
docker exec -i postgres psql -U postgres < create_database.sql
```