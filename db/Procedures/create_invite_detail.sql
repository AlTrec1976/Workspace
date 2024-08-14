CREATE OR REPLACE PROCEDURE public.create_invite_detail(IN users_comments character varying, IN inviteid uuid, IN userid uuid)
 LANGUAGE plpgsql
AS $procedure$
BEGIN
    INSERT INTO public.invite_details (invite_id, user_id, user_comments)
    VALUES (inviteid, userid, users_comments);
	
EXCEPTION
	WHEN others THEN
		ROLLBACK;
		RAISE;
END;
$procedure$
;