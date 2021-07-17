# clean all cache folders
Get-ChildItem ../../sources/ -include 'bin','obj','_ReSharper.Caches','.vs' -Force -Recurse | foreach ($_) { remove-item $_.fullname -Force -Recurse -ErrorAction SilentlyContinue -Verbose}

# remove binaries
Get-ChildItem ../../bin/ -Force -Recurse | foreach ($_) { remove-item $_.fullname -Force -Recurse -ErrorAction SilentlyContinue -Verbose}