CREATE TABLE public.workspacemart (
	id uuid DEFAULT gen_random_uuid() NOT NULL,
	"name" varchar(128) NULL,
	ownerid uuid NULL,
	CONSTRAINT workspacemart_pkey PRIMARY KEY (id)
);