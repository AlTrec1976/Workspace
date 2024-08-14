CREATE OR REPLACE PROCEDURE public.delete_task(
	taskid uuid)
LANGUAGE 'plpgsql'

AS $BODY$
BEGIN
DELETE FROM tasks WHERE id = taskid;
END;
$BODY$;