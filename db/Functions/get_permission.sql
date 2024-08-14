DROP FUNCTION IF EXISTS public.get_permission(uuid);

CREATE OR REPLACE FUNCTION public.get_permission(
	pid uuid)
    RETURNS TABLE(prid integer) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
	RETURN QUERY SELECT pr.id
	FROM users u
	LEFT JOIN roles r ON u.role = r.id
	LEFT JOIN roles_permissions rp ON r.id=rp.roleid
	LEFT JOIN permissions pr ON rp.permissionid=pr.id
	WHERE u.id = pid;
END;
$BODY$;