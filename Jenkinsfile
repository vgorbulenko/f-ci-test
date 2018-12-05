
env.wsPath = "C:\\Jenkins_workspace\\test-pipeline"

def receive() {
  env.event_type = request.headers["X-GitHub-Event"]
  payload    = request.body
}
//env.eventType = event_type

node('master') {
    ws( env.wsPath ) {
        stage('Checkout on master') {
			checkout scm
//            checkout([$class: 'GitSCM', branches: [[name: BRANCH_NAME]], doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [], userRemoteConfigs: [[credentialsId: 'github-ssh', url: 'git@github.com:frustumInc/generate.git']]]) 
//          checkout([$class: 'GitSCM', branches: [[name: gitlabSourceBranch]], doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [], userRemoteConfigs: [[credentialsId: 'vgorbulenko_https_github', url: 'https://gitlab.amcbridge.com/spronyuk/pipeline-dev-repo']]])
		}

        stage('test stage. Looking for variables') {

    		bat """ echo event_type = %env.event_type%   """
    		bat """ echo env.wsPath = $env.wsPath   """

    		bat """ echo env.BRANCH_NAME = ${BRANCH_NAME} """
			bat """ echo env.BRANCH_NAME = %BRANCH_NAME% """

			bat """ echo env.GIT_BRANCH = %GIT_BRANCH% """
			bat """ echo env.CHANGE_ID =  %CHANGE_ID% """
			bat """ echo env.CHANGE_URL = %CHANGE_URL% """
			bat """ echo env.CHANGE_TITLE =  %CHANGE_TITLE% """
			bat """ echo env.CHANGE_AUTHOR =  %CHANGE_AUTHOR% """
			bat """ echo env.CHANGE_AUTHOR_DISPLAY_NAME =  %CHANGE_AUTHOR_DISPLAY_NAME% """
			bat """ echo env.CHANGE_AUTHOR_EMAIL =  %CHANGE_AUTHOR_EMAIL% """
			bat """ echo env.CHANGE_TARGET =  %CHANGE_TARGET% """

			bat """ echo env.BUILD_NUMBER = ${BUILD_NUMBER} """
			bat """ echo env.BUILD_ID = ${BUILD_ID} """
			bat """ echo env.BUILD_DISPLAY_NAME = ${BUILD_DISPLAY_NAME} """
			bat """ echo env.JOB_NAME = ${JOB_NAME} """
			bat """ echo env.JOB_BASE_NAME = ${JOB_BASE_NAME} """
			bat """ echo env.BUILD_TAG = ${BUILD_TAG} """
			bat """ echo env.EXECUTOR_NUMBER = ${EXECUTOR_NUMBER} """
			bat """ echo env.NODE_NAME = ${NODE_NAME} """
			bat """ echo env.NODE_LABELS = ${NODE_LABELS} """
			bat """ echo env.WORKSPACE = ${WORKSPACE} """
			bat """ echo env.JENKINS_HOME = ${JENKINS_HOME} """
			bat """ echo env.JENKINS_URL = ${JENKINS_URL} """
			bat """ echo env.BUILD_URL = ${BUILD_URL} """
			bat """ echo env.JOB_URL = ${JOB_URL} """

			bat """ echo env.GIT_COMMIT = %GIT_COMMIT% """
			bat """ echo env.GIT_PREVIOUS_COMMIT = %GIT_PREVIOUS_COMMIT% """
			bat """ echo env.GIT_PREVIOUS_SUCCESSFUL_COMMIT = %GIT_PREVIOUS_SUCCESSFUL_COMMIT% """
			bat """ echo env.GIT_BRANCH = %GIT_BRANCH% """
			bat """ echo env.GIT_LOCAL_BRANCH = %GIT_LOCAL_BRANCH% """
			bat """ echo env.GIT_URL = %GIT_URL% """
			bat """ echo env.GIT_COMMITTER_NAME = %GIT_COMMITTER_NAME% """
			bat """ echo env.GIT_AUTHOR_NAME = %GIT_AUTHOR_NAME% """
			bat """ echo env.GIT_COMMITTER_EMAIL = %GIT_COMMITTER_EMAIL% """
			bat """ echo env.GIT_AUTHOR_EMAIL = %GIT_AUTHOR_EMAIL% """


			//bat """ echo env.eventType = %env.eventType%   ${env.eventType}    """
//			bat """ echo env.GIT_COMMIT = ${GIT_COMMIT} """
//			bat """ echo env.GIT_TAG = ${GIT_TAG} """
//bat """ echo payload = ${payload.ref} """
			bat """ echo -------------- """
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