#!/bin/bash          
Solutions=("../../solutions/Waves.UI.Avalonia.sln")

# Colors (EC - enable color, DC - disable color)
EC='\033[1;36m'
DC='\033[0m'

cd ../../submodules/ui/submodules/core/build/scripts/
sh clean.sh
sh build.sh
cd ../../../../build/scripts/

sh clean.sh
sh build.sh
cd ../../../../build/scripts/

cd ../../submodules/ui.showcase.common/submodules/ui/submodules/core/build/scripts/
sh clean.sh
sh build.sh
cd ../../../../build/scripts/

sh clean.sh
sh build.sh
cd ../../../../build/scripts/

sh clean.sh
sh build.sh
cd ../../../../build/scripts/

for Solution in ${Solutions[@]}; do
	echo "${EC}Cleaning...${DC}" 
	dotnet clean $Solution --configuration Release
	echo "------------------------------------------"

	echo "${EC}Restoring...${DC}" 
	dotnet restore $Solution
	echo "------------------------------------------"

	# Building docfx only compatible with Windows, so we set BuildDocFx to false.
	echo "${EC}Building...${DC}" 
	BuildDocFx=false dotnet build $Solution --no-restore --configuration Release
done
