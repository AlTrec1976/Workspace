CREATE OR REPLACE PROCEDURE public.update_task_employee(IN taskid uuid, IN temployee uuid)
 LANGUAGE plpgsql
AS $procedure$
BEGIN
	UPDATE tasks SET employeeid=temployee WHERE id=taskid;
END;
$procedure$
;