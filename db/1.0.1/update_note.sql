CREATE OR REPLACE PROCEDURE public.update_note(
	noteid uuid,
	notetext character,
	iduser uuid)
    LANGUAGE 'plpgsql'

AS $BODY$
BEGIN
UPDATE notes SET note=notetext, userid=iduser WHERE id=noteid;
END;
$BODY$;