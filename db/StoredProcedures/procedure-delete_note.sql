CREATE OR REPLACE PROCEDURE public.delete_note(
	noteid uuid)
LANGUAGE 'plpgsql'

AS $BODY$
BEGIN
DELETE FROM notes WHERE id = noteid;
END;
$BODY$;