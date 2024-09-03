CREATE OR REPLACE PROCEDURE public.update_task_status(IN taskid uuid, IN tstatus int4)
 LANGUAGE plpgsql
AS $procedure$
BEGIN
UPDATE tasks SET status=tstatus WHERE id=taskid;
END;
$procedure$;