CREATE OR REPLACE PROCEDURE public.delete_user(
	userid uuid)
    LANGUAGE 'plpgsql'

AS $BODY$
BEGIN
DELETE FROM public.users WHERE id = userid;
END;
$BODY$;