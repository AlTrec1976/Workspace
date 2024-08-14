CREATE OR REPLACE FUNCTION public.get_mart_tasks(wmart_id uuid)
 RETURNS TABLE(taskid uuid, taskname uuid, taskstatus character varying)
 LANGUAGE plpgsql
AS $function$
begin
	return query
		select 
			t.id 		as taskid
			,t.name 	as taskname
			,t.status 	as taskstatus
		from workspacemart_tasks wt  
		right join tasks t on t.id = wt.taskid and t.employeeid is null and t.status = 1
		where wt.martid  = wmart_id;
end;
$function$
;
