CREATE OR REPLACE PROCEDURE public.create_invite(IN inv_comments character varying, IN martid uuid, IN isopened boolean, OUT invite_id uuid, OUT userid uuid)
 LANGUAGE plpgsql
AS $procedure$
BEGIN
	SELECT ownerid INTO userid FROM workspacemart WHERE id=martid;
    INSERT INTO public.invites (invite_comments, mart_id,user_id, opened)
    VALUES (inv_comments, martid, userid, isopened)
    RETURNING id INTO invite_id;
	
EXCEPTION
	WHEN others THEN
		ROLLBACK;
		RAISE;
END;
$procedure$
;
