DROP TABLE IF EXISTS public.roles_permissions;

CREATE TABLE IF NOT EXISTS public.roles_permissions
(
    roleid integer NOT NULL,
    permissionid integer NOT NULL,
    CONSTRAINT fk_permissions FOREIGN KEY (permissionid)
        REFERENCES public.permissions (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT fk_roles FOREIGN KEY (roleid)
        REFERENCES public.roles (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

INSERT INTO public.roles_permissions(roleid, permissionid) VALUES (1, 1);
INSERT INTO public.roles_permissions(roleid, permissionid) VALUES (1, 2);
INSERT INTO public.roles_permissions(roleid, permissionid) VALUES (1, 3);
INSERT INTO public.roles_permissions(roleid, permissionid) VALUES (1, 4);
INSERT INTO public.roles_permissions(roleid, permissionid) VALUES (2, 1);