CREATE OR REPLACE PROCEDURE public.create_task_mart(IN mart_id uuid, IN task_name character, IN task_status integer, OUT task_id uuid, OUT owner_id uuid)
 LANGUAGE plpgsql
AS $procedure$

BEGIN
	SELECT ownerid INTO owner_id FROM workspacemart WHERE id=mart_id;

	INSERT INTO public.tasks (name, status, managerid) 
		VALUES (task_name, task_status, owner_id)
	RETURNING id INTO task_id;

	INSERT INTO public.workspacemart_tasks (martid,taskid)
		VALUES (mart_id,task_id);
	
EXCEPTION
	WHEN others THEN
		ROLLBACK;
		RAISE;
END;
$procedure$
;