CREATE OR REPLACE FUNCTION public.get_all_invites()
 RETURNS TABLE(inviteid uuid, martid uuid, userid uuid, isopened boolean, martname character varying, invitecomments character varying, username text)
 LANGUAGE plpgsql
AS $function$
begin 
	return query
	select 
		 i.id as inviteid
		,i.mart_id as martid
		,i.user_id as userid
		,i.opened as isopened
		,w.name as martname
		,i.invite_comments as invitecomments
		,u.name || ' ' || u.surname as username
	from invites i
	left join workspacemart w on i.mart_id = w.id  
	left join users u on i.user_id = u.id;
end; 
$function$
;
