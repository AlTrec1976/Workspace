CREATE OR REPLACE PROCEDURE public.create_note(IN task_id uuid, IN note_text character, IN note_user uuid, OUT note_id uuid)
    LANGUAGE plpgsql
AS $procedure$

BEGIN
INSERT INTO public.notes (note, userid)
VALUES (note_text, note_user)
    RETURNING id INTO note_id;

INSERT INTO public.task_notes (taskid,noteid)
VALUES (task_id,note_id);

EXCEPTION
    WHEN others THEN
        ROLLBACK;
        RAISE;
END;
$procedure$
;
