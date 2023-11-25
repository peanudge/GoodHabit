# Development Environment

```bash
cd ./docker
docker compose up
```

Access .NET Environment using /bin/bash

```bash
docker exec -it dev-env /bin/bash
```

Access SQL Server 2022 using /bin/bash

```bash
docker exec -it sql_server /bin/bash

$ /opt/mssql-tools/bin/sqlcmd -S localhost -U SA
$ Password: (input password)
```
