-- Database generated with pgModeler (PostgreSQL Database Modeler).
-- pgModeler  version: 0.9.1
-- PostgreSQL version: 10.0
-- Project Site: pgmodeler.io
-- Model Author: ---


-- Database creation must be done outside a multicommand file.
-- These commands were put in this file only as a convenience.
-- -- object: new_database | type: DATABASE --
-- -- DROP DATABASE IF EXISTS new_database;
-- CREATE DATABASE new_database;
-- -- ddl-end --
-- 

-- object: public.patient | type: TABLE --
-- DROP TABLE IF EXISTS public.patient CASCADE;
CREATE TABLE mobaspace_data.patient(
	id_patient bigserial NOT NULL,
	nom text NOT NULL,
	prenom text NOT NULL,
	chambre text NOT NULL,
	lit text NOT NULL,
	CONSTRAINT patient_pk PRIMARY KEY (id_patient)

);
-- ddl-end --
ALTER TABLE mobaspace_data.patient OWNER TO mobaspace_adm;
GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE mobaspace_data.patient TO mobaspace_web_usr;
GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE mobaspace_data.patient TO mobaspace_app_usr;
-- ddl-end --

-- object: public.type_capteur | type: TABLE --
-- DROP TABLE IF EXISTS public.type_capteur CASCADE;
CREATE TABLE mobaspace_data.type_capteur(
	id_type_capteur bigserial NOT NULL,
	nom text NOT NULL,
	description text NOT NULL,
	valeurs_defaut jsonb NOT NULL,
	CONSTRAINT type_capteur_pk PRIMARY KEY (id_type_capteur)

);
-- ddl-end --
ALTER TABLE mobaspace_data.type_capteur OWNER TO mobaspace_adm;
GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE mobaspace_data.type_capteur TO mobaspace_web_usr;
GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE mobaspace_data.type_capteur TO mobaspace_app_usr;
-- ddl-end --

-- object: public.capteur | type: TABLE --
-- DROP TABLE IF EXISTS public.capteur CASCADE;
CREATE TABLE mobaspace_data.capteur(
	id_capteur bigserial NOT NULL,
	id_type_capteur bigint NOT NULL,
	description text NOT NULL,
	en_marche boolean NOT NULL DEFAULT True,
	num_serie text NOT NULL,
	informations jsonb NOT NULL,
	CONSTRAINT capteur_pk PRIMARY KEY (id_capteur)

);
-- ddl-end --
ALTER TABLE mobaspace_data.capteur OWNER TO mobaspace_adm;
GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE mobaspace_data.capteur TO mobaspace_web_usr;
GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE mobaspace_data.capteur TO mobaspace_app_usr;
-- ddl-end --

-- object: public.patient_capteur | type: TABLE --
-- DROP TABLE IF EXISTS public.patient_capteur CASCADE;
CREATE TABLE mobaspace_data.patient_capteur(
	id_patient bigint NOT NULL,
	id_capteur bigint NOT NULL,
	alarme boolean NOT NULL DEFAULT False,
	regles jsonb NOT NULL,
	CONSTRAINT patient_capteur_pk PRIMARY KEY (id_patient,id_capteur)

);
-- ddl-end --
ALTER TABLE mobaspace_data.patient_capteur OWNER TO mobaspace_adm;
GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE mobaspace_data.patient_capteur TO mobaspace_web_usr;
GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE mobaspace_data.patient_capteur TO mobaspace_app_usr;
-- ddl-end --

-- object: capteur_type_capteur_fk | type: CONSTRAINT --
-- ALTER TABLE public.capteur DROP CONSTRAINT IF EXISTS capteur_type_capteur_fk CASCADE;
ALTER TABLE mobaspace_data.capteur ADD CONSTRAINT capteur_type_capteur_fk FOREIGN KEY (id_type_capteur)
REFERENCES mobaspace_data.type_capteur (id_type_capteur) MATCH FULL
ON DELETE CASCADE ON UPDATE CASCADE;
-- ddl-end --

-- object: patient_capteur_patient_fk | type: CONSTRAINT --
-- ALTER TABLE public.patient_capteur DROP CONSTRAINT IF EXISTS patient_capteur_patient_fk CASCADE;
ALTER TABLE mobaspace_data.patient_capteur ADD CONSTRAINT patient_capteur_patient_fk FOREIGN KEY (id_patient)
REFERENCES mobaspace_data.patient (id_patient) MATCH FULL
ON DELETE CASCADE ON UPDATE CASCADE;
-- ddl-end --

-- object: patient_capteur_capteur_fk | type: CONSTRAINT --
-- ALTER TABLE public.patient_capteur DROP CONSTRAINT IF EXISTS patient_capteur_capteur_fk CASCADE;
ALTER TABLE mobaspace_data.patient_capteur ADD CONSTRAINT patient_capteur_capteur_fk FOREIGN KEY (id_capteur)
REFERENCES mobaspace_data.capteur (id_capteur) MATCH FULL
ON DELETE CASCADE ON UPDATE CASCADE;
-- ddl-end --


