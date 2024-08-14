CREATE TABLE public.sendbox (
	invite_id uuid NOT NULL,
	mart_id uuid NOT NULL,
	user_id uuid NOT NULL
);

ALTER TABLE public.sendbox ADD CONSTRAINT sendbox_invites_fk FOREIGN KEY (invite_id) REFERENCES public.invites(id);
ALTER TABLE public.sendbox ADD CONSTRAINT sendbox_users_fk FOREIGN KEY (user_id) REFERENCES public.users(id);
ALTER TABLE public.sendbox ADD CONSTRAINT sendbox_workspacemart_fk FOREIGN KEY (mart_id) REFERENCES public.workspacemart(id);