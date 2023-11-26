import re

ignored_files = ['blazor.webassembly' , 'service-worker']
include_extensions = ['css' , 'js']


# Read the contents of the index.html file
with open('index.html', 'r') as file:
    html = file.read()

# Find all CSS and JS file references with a version number
file_refs = re.findall(r'(src|href)\=\"(.*)\.(json|css|js)((\?v=(\d+))?)', html)


for fl in file_refs:
    print("found : " , fl)


for type , path , extension , ver , _ , nv_str in file_refs:
    old_file = f'{path}.{extension}{ver}'

    should_ignore = any(igf in old_file for igf in ignored_files)
    should_ignore = should_ignore or (not any(file_ext == extension for file_ext in include_extensions))
    if (should_ignore):
        continue

    if nv_str == '':
        nv_str = '1'

    if (ver == ''):
        ver = '?v=1'

    new_version = f'{int(nv_str) + 1}'
    ver = ver.replace(nv_str , new_version)

    new_file = f'{path}.{extension}{ver}'

    print(old_file , '->' , new_file )
    html = html.replace(old_file , new_file) 

with open('index.html', 'w') as file:
    file.write(html)
