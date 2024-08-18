create or replace function public.get_mart_tasks(in wmart_id uuid)
	returns table (id uuid, name varchar(50), status integer)
	language plpgsql
as $function$
begin
	return query
		select 
			 t.id 		as id
			,t.name 	as name
			,t.status 	as status
		from workspacemart_tasks wt  
		right join tasks t on t.id = wt.taskid and t.employeeid is null and t.status = 1
		where wt.martid  = wmart_id;
end;
$function$;