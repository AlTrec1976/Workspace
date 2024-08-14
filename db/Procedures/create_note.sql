CREATE OR REPLACE PROCEDURE public.create_note(
	notetext character,
	iduser uuid,
	idtask uuid)
    LANGUAGE 'plpgsql'

AS $BODY$
BEGIN
INSERT INTO public.notes (note, userid, taskid) VALUES (notetext, iduser, idtask);
END;
$BODY$;