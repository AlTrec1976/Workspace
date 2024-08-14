CREATE OR REPLACE FUNCTION public.get_sendbox_users(wmart_id uuid)
 RETURNS TABLE(martid uuid, userid uuid, martname character varying, username text)
 LANGUAGE plpgsql
AS $function$
begin
	return query
		select 
			w.id  			as martid
			,u.id  			as userid
			,w.name  		as martname
			,u.name || ' ' ||u.surname 	as username
		from sendbox s
		left join workspacemart w on s.mart_id = w.id
		left join users u on s.user_id=u.id
		where s.mart_id = wmart_id;
end;
$function$
;