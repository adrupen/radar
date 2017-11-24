//Maya ASCII 2018 scene
//Name: donut.ma
//Last modified: Fri, Nov 24, 2017 03:55:44 PM
//Codeset: 1252
requires maya "2018";
currentUnit -l centimeter -a degree -t film;
fileInfo "application" "maya";
fileInfo "product" "Maya 2018";
fileInfo "version" "2018";
fileInfo "cutIdentifier" "201706261615-f9658c4cfc";
fileInfo "osv" "Microsoft Windows 8 Home Premium Edition, 64-bit  (Build 9200)\n";
fileInfo "license" "student";
createNode transform -n "pCylinder4";
	rename -uid "4221BFB8-4939-453A-33CB-0B8C312FEC0C";
	setAttr ".rp" -type "double3" -1.1920928955078125e-07 0 -1.7881393432617188e-07 ;
	setAttr ".sp" -type "double3" -1.1920928955078125e-07 0 -1.7881393432617188e-07 ;
createNode mesh -n "pCylinderShape4" -p "pCylinder4";
	rename -uid "0EA34769-431B-42B4-2EEC-8380FC49497C";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.49999998509883881 0.50000011920928955 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 40 ".uvst[0].uvsp[0:39]" -type "float2" 1 0.4208076 1 0.57919204
		 0.91718519 0.56607556 0.91718531 0.4339242 0.95105648 0.72982466 0.87634826 0.69175887
		 0.85796034 0.85796034 0.7986716 0.79867148 0.72982466 0.95105636 0.69175887 0.87634814
		 0.57919204 0.99999988 0.56607544 0.91718519 0.42080766 0.99999976 0.4339242 0.91718507
		 0.2701751 0.95105624 0.30824089 0.87634814 0.14203948 0.85796022 0.20132822 0.79867148
		 0.04894346 0.72982454 0.12365168 0.69175887 0 0.57919204 0.082814634 0.56607544 0
		 0.4208076 0.082814753 0.4339242 0.04894352 0.2701751 0.12365168 0.30824089 0.14203954
		 0.14203942 0.20132828 0.20132828 0.27017516 0.0489434 0.30824095 0.12365168 0.42080766
		 -5.9604645e-08 0.43392426 0.082814693 0.57919204 -5.9604645e-08 0.56607556 0.082814693
		 0.72982454 0.04894346 0.69175887 0.12365168 0.85796022 0.14203942 0.79867148 0.20132822
		 0.95105624 0.2701751 0.87634802 0.30824089;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 40 ".vt[0:39]"  0.95105714 0 -0.30901718 0.80901754 0 -0.5877856
		 0.5877856 0 -0.80901748 0.30901715 0 -0.95105702 0 0 -1.000000476837 -0.30901715 0 -0.95105696
		 -0.58778548 0 -0.8090173 -0.80901724 0 -0.58778542 -0.95105678 0 -0.30901706 -1.000000238419 0 0
		 -0.95105678 0 0.30901706 -0.80901718 0 0.58778536 -0.58778536 0 0.80901712 -0.30901706 0 0.95105666
		 -2.9802322e-08 0 1.000000119209 0.30901697 0 0.9510566 0.58778524 0 0.80901706 0.809017 0 0.5877853
		 0.95105654 0 0.309017 1 0 0 0.79353404 0 -0.25783494 0.67502034 0 -0.49043107 0.49043095 0 -0.6750204
		 0.25783485 0 -0.79353398 -1.9744556e-08 0 -0.83437091 -0.25783491 0 -0.79353392 -0.49043098 0 -0.67502022
		 -0.67502016 0 -0.49043092 -0.79353386 0 -0.25783482 -0.83437079 0 -2.9616812e-08
		 -0.79353386 0 0.25783476 -0.6750201 0 0.49043071 -0.49043086 0 0.67502004 -0.25783485 0 0.79353362
		 -4.4610726e-08 0 0.83437055 0.2578347 0 0.79353356 0.49043074 0 0.67501998 0.67501992 0 0.49043071
		 0.79353362 0 0.2578347 0.83437055 0 -2.9616812e-08;
	setAttr -s 60 ".ed[0:59]"  0 1 0 1 2 0 2 3 0 3 4 0 4 5 0 5 6 0 6 7 0
		 7 8 0 8 9 0 9 10 0 10 11 0 11 12 0 12 13 0 13 14 0 14 15 0 15 16 0 16 17 0 17 18 0
		 18 19 0 19 0 0 0 20 0 1 21 0 20 21 0 2 22 0 21 22 0 3 23 0 22 23 0 4 24 0 23 24 0
		 5 25 0 24 25 0 6 26 0 25 26 0 7 27 0 26 27 0 8 28 0 27 28 0 9 29 0 28 29 0 10 30 0
		 29 30 0 11 31 0 30 31 0 12 32 0 31 32 0 13 33 0 32 33 0 14 34 0 33 34 0 15 35 0 34 35 0
		 16 36 0 35 36 0 17 37 0 36 37 0 18 38 0 37 38 0 19 39 0 38 39 0 39 20 0;
	setAttr -s 20 -ch 80 ".fc[0:19]" -type "polyFaces" 
		f 4 0 21 -23 -21
		mu 0 4 0 1 2 3
		f 4 1 23 -25 -22
		mu 0 4 1 4 5 2
		f 4 2 25 -27 -24
		mu 0 4 4 6 7 5
		f 4 3 27 -29 -26
		mu 0 4 6 8 9 7
		f 4 4 29 -31 -28
		mu 0 4 8 10 11 9
		f 4 5 31 -33 -30
		mu 0 4 10 12 13 11
		f 4 6 33 -35 -32
		mu 0 4 12 14 15 13
		f 4 7 35 -37 -34
		mu 0 4 14 16 17 15
		f 4 8 37 -39 -36
		mu 0 4 16 18 19 17
		f 4 9 39 -41 -38
		mu 0 4 18 20 21 19
		f 4 10 41 -43 -40
		mu 0 4 20 22 23 21
		f 4 11 43 -45 -42
		mu 0 4 22 24 25 23
		f 4 12 45 -47 -44
		mu 0 4 24 26 27 25
		f 4 13 47 -49 -46
		mu 0 4 26 28 29 27
		f 4 14 49 -51 -48
		mu 0 4 28 30 31 29
		f 4 15 51 -53 -50
		mu 0 4 30 32 33 31
		f 4 16 53 -55 -52
		mu 0 4 32 34 35 33
		f 4 17 55 -57 -54
		mu 0 4 34 36 37 35
		f 4 18 57 -59 -56
		mu 0 4 36 38 39 37
		f 4 19 20 -60 -58
		mu 0 4 38 0 3 39;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
select -ne :time1;
	setAttr ".o" 1;
	setAttr ".unw" 1;
select -ne :hardwareRenderingGlobals;
	setAttr ".otfna" -type "stringArray" 22 "NURBS Curves" "NURBS Surfaces" "Polygons" "Subdiv Surface" "Particles" "Particle Instance" "Fluids" "Strokes" "Image Planes" "UI" "Lights" "Cameras" "Locators" "Joints" "IK Handles" "Deformers" "Motion Trails" "Components" "Hair Systems" "Follicles" "Misc. UI" "Ornaments"  ;
	setAttr ".otfva" -type "Int32Array" 22 0 1 1 1 1 1
		 1 1 1 0 0 0 0 0 0 0 0 0
		 0 0 0 0 ;
	setAttr ".fprt" yes;
select -ne :renderPartition;
	setAttr -s 3 ".st";
select -ne :renderGlobalsList1;
select -ne :defaultShaderList1;
	setAttr -s 5 ".s";
select -ne :postProcessList1;
	setAttr -s 2 ".p";
select -ne :defaultRenderingList1;
select -ne :initialShadingGroup;
	setAttr -s 10 ".dsm";
	setAttr ".ro" yes;
	setAttr -s 6 ".gn";
select -ne :initialParticleSE;
	setAttr ".ro" yes;
select -ne :defaultResolution;
	setAttr ".pa" 1;
select -ne :hardwareRenderGlobals;
	setAttr ".ctrs" 256;
	setAttr ".btrs" 512;
connectAttr "pCylinderShape4.iog" ":initialShadingGroup.dsm" -na;
// End of donut.ma
