CREATE OR REPLACE PROCEDURE public.create_workspacemart(IN name_w character varying, IN owner_id uuid, OUT mart_id uuid)
 LANGUAGE plpgsql
AS $procedure$
BEGIN
    INSERT INTO public.workspacemart (name, ownerid)
    VALUES (name_w, owner_id)
    RETURNING id INTO mart_id;
END;
$procedure$
;
