CREATE OR REPLACE PROCEDURE public.delete_note(
    IN note_id uuid)
    LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
    DELETE FROM task_notes WHERE noteid = note_id;
    DELETE FROM notes WHERE id = note_id;
END;
$BODY$;