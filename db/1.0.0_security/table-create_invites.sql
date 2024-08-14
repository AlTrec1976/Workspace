CREATE TABLE public.invites (
	id uuid DEFAULT gen_random_uuid() NOT NULL,
	invite_comments varchar(200) NULL,
	mart_id uuid NOT NULL,
	user_id uuid NOT NULL,
	opened bool NULL,
	CONSTRAINT invites_pkey PRIMARY KEY (id)
);

ALTER TABLE public.invites ADD CONSTRAINT invites_users_fk FOREIGN KEY (user_id) REFERENCES public.users(id);
ALTER TABLE public.invites ADD CONSTRAINT invites_workspacemart_fk FOREIGN KEY (mart_id) REFERENCES public.workspacemart(id);