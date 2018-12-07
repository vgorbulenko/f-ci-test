
env.wsPath = "C:\\Jenkins_workspace\\test-pipeline"
env.bucketName = "s3://frustum-installer"
env.GenerateBuildVersion = "0.0.0"
env.GenerateBuildStage = "0"
//if GenerateBuildRelease == 1 -- sing the installers
env.GenerateBuildRelease = "0"

node('master') {
	ws( env.wsPath ) {
		stage ('Initial stage') {
			if ( BRANCH_NAME == "master" ) {
				env.GenerateBuildStage="qa"
				env.GenerateBuildRelease="0"
			}
			if ( BRANCH_NAME == "release" ) {
				env.GenerateBuildStage="rc"
				env.GenerateBuildRelease="0"
			}
			if ( BRANCH_NAME.startsWith('r-') ) {
				env.GenerateBuildStage=""
				env.GenerateBuildRelease="1"
			}
			bat """
				@echo off
				echo ==== Current branch name is %BRANCH_NAME% ====
				echo ==== GenerateBuildStage == %GenerateBuildStage% ====
				echo ==== GenerateBuildRelease == %GenerateBuildRelease% ====
			"""

		}
	}
}

//clone repos and checkout
if (BRANCH_NAME == "master" || BRANCH_NAME == "release" || BRANCH_NAME.startsWith('r-')) {
	node('master') {
		ws( env.wsPath ) {
			stage('Checkout on master') {
				def scmVars = checkout scm
				env.GIT_URL = scmVars.GIT_URL
			}
		}
	}
}	

//Versioning
// master release stage
if (BRANCH_NAME == "master" || BRANCH_NAME == "release" ) {
	node('master') {
		ws( env.wsPath ) {
			stage('Versioning') {
			    bat """
				    @echo off
					echo ===Versioning===
					SET VERSION_PATH=%WORKSPACE%\\Generate\\include\\version.h
					python %SCRIPTS-DIR%\\add-build-number-to-version-h.py %VERSION_PATH% %BUILD_NUMBER% 2>tmp_version.txt
					exit 0
					"""
				env.GenerateBuildVersion=readFile('tmp_version.txt').trim()
				bat """
				@echo off
					SET VERSION_PATH=%WORKSPACE%\\Generate.WebInstaller\\Version.cs
					python %SCRIPTS-DIR%\\UpdateVersion-in-cs-from-source.py %VERSION_PATH% %GenerateBuildVersion% 2>tmp_version_cs.txt
					exit 0
				"""
				bat """
					@echo off
					echo ==== GenerateBuildVersion = %GenerateBuildVersion% =====
				"""
			}
		}
	}

}
//Versioning
// r- stage
if (BRANCH_NAME.startsWith('r-')) {
	node('master') {
		ws( env.wsPath ) {
			stage('Versioning') {
				env.GenerateBuildVersion=BRANCH_NAME.replace('r-','')
				env.bn=GenerateBuildVersion.split("\\.")[2]
				bat """
					@echo off
					SET VERSION_PATH=%WORKSPACE%\\Generate\\include\\version.h
					python %SCRIPTS-DIR%\\add-build-number-to-version-h.py %VERSION_PATH% $env.bn 2>tmp_version.txt
					SET VERSION_PATH=%WORKSPACE%\\Generate.WebInstaller\\Version.cs
					python %SCRIPTS-DIR%\\UpdateVersion-in-cs-from-source.py %VERSION_PATH% %GenerateBuildVersion% 2>tmp_version_cs.txt
					echo ==== GenerateBuildVersion = %GenerateBuildVersion% ====
					exit 0
				"""
			}
		}
	}
}

//packing and copy repo to slave. Both branches and r-
if (BRANCH_NAME == "master" || BRANCH_NAME == "release" || BRANCH_NAME.startsWith('r-')) {
	node('master') {
		ws( env.wsPath ) {
			stage('Preparing archive with source code') {
				bat """ 
					@echo off
					echo ==== Cleaning up ====
					rd /Q /S %WORKSPACE%\\build
					rd /Q /S %WORKSPACE%\\sources > nul 2>&1				
					mkdir %WORKSPACE%\\sources
					C:\\Progra~1\\7-Zip\\7z.exe a %WORKSPACE%\\sources\\%BRANCH_NAME%_%GenerateBuildVersion%.zip %WORKSPACE%\\*
					exit 0
				"""
			}
			stage('Transfer sources to S3 temp bucket') {
				bat """
					@echo off
					aws s3 rm s3://frustum-temp/temp --recursive
				"""
				bat """
					@echo off
					aws s3 cp %WORKSPACE%\\sources\\%BRANCH_NAME%_%GenerateBuildVersion%.zip s3://frustum-temp/temp/%BRANCH_NAME%_%GenerateBuildVersion%.zip --sse
				"""
			}
	
		}
	}
}


//building on slave and deploying results
if (BRANCH_NAME == "master" || BRANCH_NAME == "release" || BRANCH_NAME.startsWith('r-')) {
	node('slave') {
		stage('Clean workspace') {
			bat """
				@echo off
				rd /Q /S %wsPath%
			"""
		}
		ws( env.wsPath ) {
			stage('Copyng sources from S3 temp bucket') {
				bat """
					@echo off
					aws s3 cp s3://frustum-temp/temp/%BRANCH_NAME%_%GenerateBuildVersion%.zip %WORKSPACE%\\%BRANCH_NAME%_%GenerateBuildVersion%.zip --sse
				"""
				bat """
					@echo off
					aws s3 rm s3://frustum-temp/temp --recursive
				"""
			}
	
			stage('Unpacking sources') {
				bat """
					@echo off
					C:\\Progra~1\\7-Zip\\7z.exe x -y %WORKSPACE%\\%BRANCH_NAME%_%GenerateBuildVersion%.zip
				"""
			}	
        
			stage('Building solution') {
				bat """
					@echo off
					echo cleaning up
					rd /Q /S %WORKSPACE%\\build
					echo ==== Re-build solution ====
					::"C:\\Program Files (x86)\\MSBuild\\14.0\\Bin\\MSBuild.exe" Generate.Release.sln /t:Rebuild /p:Configuration=BuildRelease /p:Platform="x64" /p:QtMsBuild="C:\\Users\\Administrator\\AppData\\Local\\QtMsBuild" 
				"""
			}	
        
			stage('Returning results') {
				if ( BRANCH_NAME == "master" || BRANCH_NAME == "release" ) {
					env.bucketName = env.bucketName+"/"+env.GenerateBuildStage
				}
				bat """
					::just testing environment variables
					@echo off
					echo ==== SLAVE ==== Current branch name is %BRANCH_NAME% ====
					echo ==== SLAVE ==== GenerateBuildStage == %GenerateBuildStage% ====
					echo ==== SLAVE ==== GenerateBuildRelease == %GenerateBuildRelease% ====
					echo ==== SLAVE ==== GenerateBuildVersion == %GenerateBuildVersion% ====
					echo ==== SLAVE ==== bucketName == %bucketName% ====
				"""
				env.dir_installers="%WORKSPACE%\\build\\bin\\x64\\BuildRelease-installer\\product"
				bat """
					@echo off
					echo ==== Publishing results to S3 bucket ====
					
					::just testing step
					mkdir %dir_installers%
					echo %GenerateBuildVersion% version.json > %dir_installers%\\version.json
					echo %GenerateBuildVersion% GENERATEInstaller.exe > %dir_installers%\\GENERATEInstaller.exe
					echo %GenerateBuildVersion% GENERATE.Bootstrapper.exe > %dir_installers%\\GENERATE.Bootstrapper.exe
					echo %GenerateBuildVersion% GENERATE.Package.exe > %dir_installers%\\GENERATE.Package.exe
					::end of just testing step

					"""
				bat """
					@echo off
					aws s3 cp %dir_installers%\\version.json %bucketName%/version.json --sse
				"""
				bat """
					@echo off
					aws s3 cp %dir_installers%\\GENERATEInstaller.exe %bucketName%/GENERATEInstaller.exe --sse
				"""
				bat """
					@echo off
					aws s3 cp %dir_installers%\\GENERATE.Bootstrapper.exe %bucketName%/%GenerateBuildVersion%/GENERATE.Bootstrapper.exe --sse
				"""
				bat """
					@echo off
					aws s3 cp %dir_installers%\\GENERATE.Package.exe %bucketName%/%GenerateBuildVersion%/GENERATE.Package.exe --sse
				"""
			}	
		}	
	}
}


if (BRANCH_NAME == "release") {
	node ('master') {
		ws (env.wsPath) {
			stage ('Push new tag to GitHub') {
			    env.GIT_URL = env.GIT_URL.replace('https://','')
				bat """
					@echo off
					echo prepare new release tag
				"""
					withCredentials([usernamePassword(credentialsId: 'vgorbulenko_token_github', passwordVariable: 'USERPASS', usernameVariable: 'USERNAME')]) {
						bat """
							@echo off
							git config --global user.email "generate-ci@frustum.io"
							git config --global user.name "Generate CI"
							git tag -a rc-$env.GenerateBuildVersion -m \'autotag\'
							git push https://%USERPASS%@%GIT_URL% rc-$env.GenerateBuildVersion
						"""
                    }

				
			}		
		}
	}
}


//node('slave') {
  //  ws( env.wsPath ) {
	//    bat """ %SCRIPTS-DIR%\\sign.bat generate C:\\test\\certificate\\test_sign_code_sert.pfx passw1234ord"""
//		bat """   %SCRIPTS-DIR%\\sign.bat msi C:\\test\\certificate\\test_sign_code_sert.pfx passw1234ord"""
	//	bat """   %SCRIPTS-DIR%\\sign.bat package C:\\test\\certificate\\test_sign_code_sert.pfx passw1234ord"""	
//	}
//}