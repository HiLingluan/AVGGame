set GEN_CLIENT=dotnet ..\Tools\Luban.ClientServer\Luban.ClientServer.dll

%GEN_CLIENT% -j cfg --^
 -d Defines\__root__.xml ^
 --input_data_dir Datas ^
 --output_data_dir output_lua ^
 --gen_types convert_lua ^
 -s all
pause