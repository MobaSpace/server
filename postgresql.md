# PostgreSQL installation

## Add repository

* Add pgp key

```bash
  wget --quiet -O - https://www.postgresql.org/media/keys/ACCC4CF8.asc | sudo apt-key add -
```

* Create apt file

`/etc/apt/sources.list.d/pgdg.list`

```
  deb http://apt.postgresql.org/pub/repos/apt/ bionic-pgdg main
```

## Install PostgreSQL 11

```bash
  sudo apt-get update
  sudo apt-get install postgresql-11
```

## Configure PostgreSQL

* in `/etc/postgresql/11/main/postgresql.conf`

```
  listen_addresses = '*'
```

* in `/etc/postgresql/11/main/pg_hba.conf`

```
  host    mobaspace    mobaspace_adm        10.0.0.0/23        md5
  host    mobaspace    mobaspace_web_usr    10.0.0.0/23        md5
  host    mobaspace    mobaspace_app_usr    10.0.0.0/23        md5
```

## Create database and users

* With `mobaspace_createdb.sql` script

```sql
  CREATE ROLE mobaspace_web_usr LOGIN
  PASSWORD ''
  NOSUPERUSER INHERIT NOCREATEDB NOCREATEROLE NOREPLICATION;

  CREATE ROLE mobaspace_app_usr LOGIN
  PASSWORD ''
  NOSUPERUSER INHERIT NOCREATEDB NOCREATEROLE NOREPLICATION;

  CREATE ROLE mobaspace_adm LOGIN
  PASSWORD ''
  NOSUPERUSER INHERIT NOCREATEDB NOCREATEROLE NOREPLICATION;


  CREATE TABLESPACE mobaspace_data OWNER mobaspace_adm LOCATION '/srv/bd';

  CREATE DATABASE mobaspace
  WITH OWNER mobaspace_adm
  ENCODING = 'UTF8'
  TABLESPACE mobaspace_data;

  GRANT ALL ON DATABASE mobaspace TO mobaspace_adm;
  GRANT CONNECT ON DATABASE mobaspace TO mobaspace_web_usr;
  GRANT CONNECT ON DATABASE mobaspace TO mobaspace_app_usr;
  REVOKE CREATE ON SCHEMA public FROM PUBLIC;

  \c mobaspace;

  CREATE SCHEMA mobaspace_data;
  SET search_path = mobaspace_data;

  ALTER SCHEMA mobaspace_data OWNER TO mobaspace_adm;

  GRANT ALL ON SCHEMA mobaspace_data TO mobaspace_adm;
  GRANT USAGE ON SCHEMA mobaspace_data TO mobaspace_web_usr;
  GRANT USAGE ON SCHEMA mobaspace_data TO mobaspace_app_usr;
  ALTER ROLE mobaspace_adm SET search_path = mobaspace_data;
  ALTER ROLE mobaspace_app_usr SET search_path = mobaspace_data;
  ALTER ROLE mobaspace_web_usr SET search_path = mobaspace_data;

```

