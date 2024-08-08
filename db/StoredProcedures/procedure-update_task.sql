CREATE OR REPLACE PROCEDURE public.update_task(
	taskid uuid,
	taskname character,
	taskstatus integer)
    LANGUAGE 'plpgsql'

AS $BODY$
BEGIN
UPDATE tasks SET name=taskname, status=taskstatus WHERE id=taskid;
END;
$BODY$;