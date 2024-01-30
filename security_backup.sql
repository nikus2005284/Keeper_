PGDMP                       |            Security    16.1    16.1 $    �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    16421    Security    DATABASE     ~   CREATE DATABASE "Security" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
    DROP DATABASE "Security";
                postgres    false            �            1259    16489    Individual_Id_seq    SEQUENCE     �   CREATE SEQUENCE public."Individual_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    MAXVALUE 2147483647
    CACHE 1;
 *   DROP SEQUENCE public."Individual_Id_seq";
       public          postgres    false            �            1259    16552    division    TABLE     _   CREATE TABLE public.division (
    id integer NOT NULL,
    info character varying NOT NULL
);
    DROP TABLE public.division;
       public         heap    postgres    false            �            1259    16603    division_id_seq    SEQUENCE     �   ALTER TABLE public.division ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.division_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    220            �            1259    16467 	   employees    TABLE       CREATE TABLE public.employees (
    id_employee integer NOT NULL,
    "firstName" character varying(50) NOT NULL,
    name character varying(50) NOT NULL,
    "lastName" character varying(50),
    division character varying(50) NOT NULL,
    department character varying(50)
);
    DROP TABLE public.employees;
       public         heap    postgres    false            �            1259    16604    employees_id_employee_seq    SEQUENCE     �   ALTER TABLE public.employees ALTER COLUMN id_employee ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.employees_id_employee_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    215            �            1259    16531 
   groupUsers    TABLE     �  CREATE TABLE public."groupUsers" (
    id integer NOT NULL,
    "applicationNumber" integer NOT NULL,
    "beginDate" date NOT NULL,
    "endDate" date NOT NULL,
    "firstName" character varying NOT NULL,
    name character varying NOT NULL,
    "lastName" character varying,
    number character varying,
    email character varying NOT NULL,
    organization character varying,
    note character varying NOT NULL,
    birthday date NOT NULL,
    passport character varying NOT NULL,
    "pdfPath" character varying NOT NULL,
    "photoPath" character varying NOT NULL,
    target character varying NOT NULL,
    division character varying NOT NULL,
    status character varying,
    comment character varying,
    employee character varying NOT NULL
);
     DROP TABLE public."groupUsers";
       public         heap    postgres    false            �            1259    16605    groupUsers_id_seq    SEQUENCE     �   ALTER TABLE public."groupUsers" ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."groupUsers_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    217            �            1259    16538    individ    TABLE     �  CREATE TABLE public.individ (
    id integer NOT NULL,
    "beginDate" date NOT NULL,
    "endDate" date NOT NULL,
    "firstName" character varying NOT NULL,
    name character varying NOT NULL,
    "lastName" character varying,
    number character varying,
    email character varying NOT NULL,
    organization character varying,
    note character varying NOT NULL,
    birthday date NOT NULL,
    passport character varying NOT NULL,
    "pdfPath" character varying NOT NULL,
    "photoPath" character varying NOT NULL,
    target character varying NOT NULL,
    division character varying NOT NULL,
    status character varying,
    comment character varying,
    employee character varying NOT NULL
);
    DROP TABLE public.individ;
       public         heap    postgres    false            �            1259    16601    individ_id_seq    SEQUENCE     �   ALTER TABLE public.individ ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.individ_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    218            �            1259    16545    target    TABLE     e   CREATE TABLE public.target (
    "idTarget" integer NOT NULL,
    info character varying NOT NULL
);
    DROP TABLE public.target;
       public         heap    postgres    false            �            1259    16606    target_idTarget_seq    SEQUENCE     �   ALTER TABLE public.target ALTER COLUMN "idTarget" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."target_idTarget_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    219            �            1259    16584    user    TABLE     �   CREATE TABLE public."user" (
    id integer NOT NULL,
    login character varying NOT NULL,
    password character varying NOT NULL,
    passport character varying NOT NULL,
    "userName" character varying NOT NULL
);
    DROP TABLE public."user";
       public         heap    postgres    false            �            1259    16583    user_id_seq    SEQUENCE     �   ALTER TABLE public."user" ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.user_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    222            �          0    16552    division 
   TABLE DATA           ,   COPY public.division (id, info) FROM stdin;
    public          postgres    false    220   -,       �          0    16467 	   employees 
   TABLE DATA           e   COPY public.employees (id_employee, "firstName", name, "lastName", division, department) FROM stdin;
    public          postgres    false    215   c,       �          0    16531 
   groupUsers 
   TABLE DATA           �   COPY public."groupUsers" (id, "applicationNumber", "beginDate", "endDate", "firstName", name, "lastName", number, email, organization, note, birthday, passport, "pdfPath", "photoPath", target, division, status, comment, employee) FROM stdin;
    public          postgres    false    217   �,       �          0    16538    individ 
   TABLE DATA           �   COPY public.individ (id, "beginDate", "endDate", "firstName", name, "lastName", number, email, organization, note, birthday, passport, "pdfPath", "photoPath", target, division, status, comment, employee) FROM stdin;
    public          postgres    false    218   �,       �          0    16545    target 
   TABLE DATA           2   COPY public.target ("idTarget", info) FROM stdin;
    public          postgres    false    219   G.       �          0    16584    user 
   TABLE DATA           K   COPY public."user" (id, login, password, passport, "userName") FROM stdin;
    public          postgres    false    222   w.       �           0    0    Individual_Id_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public."Individual_Id_seq"', 1, false);
          public          postgres    false    216            �           0    0    division_id_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public.division_id_seq', 1, false);
          public          postgres    false    224            �           0    0    employees_id_employee_seq    SEQUENCE SET     H   SELECT pg_catalog.setval('public.employees_id_employee_seq', 1, false);
          public          postgres    false    225            �           0    0    groupUsers_id_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public."groupUsers_id_seq"', 1, true);
          public          postgres    false    226            �           0    0    individ_id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('public.individ_id_seq', 3, true);
          public          postgres    false    223            �           0    0    target_idTarget_seq    SEQUENCE SET     D   SELECT pg_catalog.setval('public."target_idTarget_seq"', 1, false);
          public          postgres    false    227            �           0    0    user_id_seq    SEQUENCE SET     9   SELECT pg_catalog.setval('public.user_id_seq', 2, true);
          public          postgres    false    221            5           2606    16471    employees Employees_pkey 
   CONSTRAINT     a   ALTER TABLE ONLY public.employees
    ADD CONSTRAINT "Employees_pkey" PRIMARY KEY (id_employee);
 D   ALTER TABLE ONLY public.employees DROP CONSTRAINT "Employees_pkey";
       public            postgres    false    215            7           2606    16537    groupUsers GroupUsers_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public."groupUsers"
    ADD CONSTRAINT "GroupUsers_pkey" PRIMARY KEY (id);
 H   ALTER TABLE ONLY public."groupUsers" DROP CONSTRAINT "GroupUsers_pkey";
       public            postgres    false    217            =           2606    16558    division division_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.division
    ADD CONSTRAINT division_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.division DROP CONSTRAINT division_pkey;
       public            postgres    false    220            9           2606    16544    individ individ_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.individ
    ADD CONSTRAINT individ_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.individ DROP CONSTRAINT individ_pkey;
       public            postgres    false    218            ;           2606    16551    target target_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.target
    ADD CONSTRAINT target_pkey PRIMARY KEY ("idTarget");
 <   ALTER TABLE ONLY public.target DROP CONSTRAINT target_pkey;
       public            postgres    false    219            ?           2606    16590    user users_pkey 
   CONSTRAINT     O   ALTER TABLE ONLY public."user"
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);
 ;   ALTER TABLE ONLY public."user" DROP CONSTRAINT users_pkey;
       public            postgres    false    222            �   &   x�3�0�bÅ}v\�~a��r��b������ Qk�      �   9   x�3�0��֋M.컰	�A���b;P ��qa;P`߅-/6�X8%�b���� �>%      �   *   x�3�4�4202�50�52Ff�e楓@Q�Bq��qqq ��.P      �   Q  x�Œ�J�@�ד��H:�dLv]�q](�5�i�K���EA@]�F��iz�μ�'A��W�������N�͸-ܟRx1c�����-L!%���dx�CJ	<�
R3*�7(�%\�<�z�D�Q�BsƪC�	WN|I�232csKZ����x�-E����������>f�'��R9�Z�~�8�6c�n�z/�N4�u�Ν^x���;�F��;�蝛k����	�g̜�soaV$**R;*ƙc�B2��.�R{�#J<�pA%��'�8oؿ*$_v�gB���H�Jf�~A��gݘ{�ڊ���	 �T� P���Kͱ,���1      �       x�3估�®��@�|�H������� �
�      �   �   x��A
�0������L&�$�٤t��T,TQ��C�W���H�����Ӱn�r�d���85�
ֲKz�[Qk�Z���&��#Ի��S��"ؔ�0]�i<_��r�Lmv�Ruڄ^;����^��C y,��䥶�R�^P./     