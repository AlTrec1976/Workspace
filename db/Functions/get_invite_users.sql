CREATE OR REPLACE FUNCTION public.get_invite_users(wmart_id uuid)
 RETURNS TABLE(inviteid uuid, martid uuid, userid uuid, isopened boolean, martname character varying, username text, invitecomments character varying)
 LANGUAGE plpgsql
AS $function$
begin
	return query
		select 
		ids.invite_id 				as inviteid
		,i.mart_id 	 				as martid
		,ids.user_id 				as userid 
		,i.opened 					as isopened
		,w.name						as martname
		,u.name	|| ' ' ||u.surname 	as username
		,ids.user_comments 			as invitecomments
	from invites i 
	left join invite_details ids on i.id = ids.invite_id 
	left join workspacemart w on i.mart_id = w.id 
	left join users u on ids.user_id = u.id
	where i.mart_id = wmart_id;
end;
$function$
;